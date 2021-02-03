using System;
using System.Collections.Generic;
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
    }
}
