using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using AutoMapper;
using FYP_WebApp.Common_Logic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.Api
{
    public class AccountController : ApiController
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private Controllers.AccountController _accountController;
        private ApplicationUserManager _userManager;
        private Mapper mapper;

        public AccountController()
        {
            _applicationDbContext = new ApplicationDbContext();
            _accountController = new Controllers.AccountController();
            var config = AutomapperConfig.instance().Configure();
            mapper = new Mapper(config);
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); ;
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Get()
        {
            var users = mapper.Map<IList<ApplicationUser>, List<ApplicationUserDto>>(_applicationDbContext.Users.ToList());

            if (users.Count > 0)
            {
                return Json(users);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "No users were found.");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Get(string id)
        {
            if (id != "")
            {
                var user = _applicationDbContext.Users.Find(id);

                if (user != null)
                {
                    return Json(mapper.Map<ApplicationUser, ApplicationUserDto>(user));
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "A user with the specified ID was not found.");
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "No ID was provided.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post([FromBody] RegisterViewModel model)
        {
            if (model != null)
            {
                var user = new ApplicationUser { Name = model.Name, Gender = model.Gender, Address = model.Address, DateOfBirth = model.DateOfBirth, PhoneNumber = model.PhoneNumber, IsAllowedToSwim = model.IsAllowedToSwim, UserName = model.Email, Email = model.Email };
                var result = UserManager.Create(user);
                if (result.Succeeded)
                {
                    if (model.Role != "")
                    {
                        UserManager.AddToRole(user.Id, model.Role);
                    }

                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        [Authorize(Roles = "Admin, Parent")]
        public IHttpActionResult Patch([FromBody] ApplicationUserDto request)
        {
            if (request == null)
            {
                return Content(HttpStatusCode.BadRequest, "Request invalid, check submitted data.");
            }

            var user = mapper.Map<ApplicationUserDto, ApplicationUser>(request);

            if (user != null)
            {
                var currentUser = _applicationDbContext.Users.Find(User.Identity.GetUserId());

                var existingUser = _applicationDbContext.Users.Find(user.Id);

                if (existingUser != null)
                {
                    if (User.IsInRole("Admin"))
                    {
                        List<string> actionsPerformed = new List<string>();

                        if (user.Name != null && user.Name != existingUser.Name)
                        {
                            var oldName = existingUser.Name;
                            existingUser.Name = user.Name;

                            _applicationDbContext.Entry(existingUser).State = EntityState.Modified;
                            _applicationDbContext.SaveChanges();

                            actionsPerformed.Add("Name updated from " + oldName + " to " + existingUser.Name);
                        }
                        if (user.FamilyGroupId != null && user.FamilyGroupId != existingUser.FamilyGroupId)
                        {
                            existingUser.FamilyGroupId = user.FamilyGroupId;

                            _applicationDbContext.Entry(existingUser).State = EntityState.Modified;
                            _applicationDbContext.SaveChanges();

                            actionsPerformed.Add("User family group updated");
                        }

                        if (actionsPerformed.Count > 0)
                        {
                            return Json(actionsPerformed);
                        }
                        else
                        {
                            return Content(HttpStatusCode.BadGateway, "No changes were specified so none were made.");
                        }

                    }
                    else
                    {

                        if (currentUser.FamilyGroupId == user.FamilyGroupId)
                        {
                            if (user.PhoneNumber != null && user.PhoneNumber != existingUser.PhoneNumber)
                            {
                                existingUser.PhoneNumber = user.PhoneNumber;

                                _applicationDbContext.Entry(existingUser).State = EntityState.Modified;
                                _applicationDbContext.SaveChanges();

                                return Content(HttpStatusCode.OK, "User Phone Number updated.");
                            }
                            else
                            {
                                return Content(HttpStatusCode.BadRequest, "No new phone number was submitted.");
                            }
                        }
                        else
                        {
                            return Content(HttpStatusCode.Unauthorized, "You do not have permission to edit this user.");
                        }

                    }
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "No user was found matching the submitted data.");
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "No data was submitted.");
            }

        }

        [Authorize(Roles="Admin")]
        [Route("api/account/lockout")]
        [HttpPatch]
        public IHttpActionResult Lockout([FromBody] ApplicationUserDto request)
        {
            if (!string.IsNullOrEmpty(request.ToString()))
            {
                var existingUser = _applicationDbContext.Users.Find(request.Id);

                if (existingUser != null)
                {
                    if (existingUser.LockoutEndDateUtc != request.LockoutEndDateUtc)
                    {
                        if (request.LockoutEndDateUtc == DateTime.MinValue)
                        {
                            existingUser.LockoutEndDateUtc = null;
                        }
                        else
                        {
                            existingUser.LockoutEndDateUtc = request.LockoutEndDateUtc;
                        }

                        _applicationDbContext.Entry(existingUser).State = EntityState.Modified;
                        _applicationDbContext.SaveChanges();
                        return Content(HttpStatusCode.OK, "User lock out date set to: " + request.LockoutEndDateUtc);
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, "No changes specified so none were made");
                    }
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "The user with the specified ID was not found.");
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "No data was submitted.");
            }
        }


    }
}
