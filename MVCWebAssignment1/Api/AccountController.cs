using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.Api
{
    public class AccountController : ApiController
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private Controllers.AccountController _accountController;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
            _applicationDbContext = new ApplicationDbContext();
            _accountController = new Controllers.AccountController();
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
            var users = _applicationDbContext.Users.ToList();

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
                    return Json(user);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
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
        public IHttpActionResult Patch([FromBody] ApplicationUser user)
        {
            if (user != null)
            {
                var currentUser = _applicationDbContext.Users.Find(User.Identity.GetUserId());
                var newUser = _applicationDbContext.Users.Find(user.Id);

                if (newUser != null)
                {
                    if (User.IsInRole("Admin"))
                    {
                        if (user.Name != null && user.Name != newUser.Name)
                        {
                            newUser.Name = user.Name;

                            _applicationDbContext.Entry(newUser).State = EntityState.Modified;
                            _applicationDbContext.SaveChanges();

                            return Content(HttpStatusCode.OK, "User Name updated.");
                        }
                        else if (user.FamilyGroupId != null && user.FamilyGroupId != newUser.FamilyGroupId)
                        {
                            newUser.FamilyGroupId = user.FamilyGroupId;

                            _applicationDbContext.Entry(newUser).State = EntityState.Modified;
                            _applicationDbContext.SaveChanges();

                            return Content(HttpStatusCode.OK, "User Family updated.");
                        }
                        else if (user.LockoutEndDateUtc != newUser.LockoutEndDateUtc)
                        {
                            newUser.LockoutEndDateUtc = user.LockoutEndDateUtc;

                            _applicationDbContext.Entry(newUser).State = EntityState.Modified;
                            _applicationDbContext.SaveChanges();

                            if (newUser.LockoutEndDateUtc != null)
                            {
                                return Content(HttpStatusCode.OK, "User has been archived and can no longer login.");
                            }
                            else
                            {
                                return Content(HttpStatusCode.OK, "User is no longer archived and can now login.");
                            }

                        }
                        else
                        {
                            return Content(HttpStatusCode.BadRequest, "No name or family group was submitted");
                        }
                        
                    }
                    else
                    {

                        if (currentUser.FamilyGroupId == user.FamilyGroupId)
                        {
                            if (user.PhoneNumber != null && user.PhoneNumber != newUser.PhoneNumber)
                            {
                                newUser.PhoneNumber = user.PhoneNumber;

                                _applicationDbContext.Entry(newUser).State = EntityState.Modified;
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

    }
}
