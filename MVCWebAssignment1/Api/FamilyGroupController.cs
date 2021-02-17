using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FYP_WebApp.Common_Logic;
using Microsoft.Owin.Security.Facebook;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;
using MVCWebAssignment1.ServiceLayer;

namespace MVCWebAssignment1.Api
{
    public class FamilyGroupController : ApiController
    {
        private readonly FamilyGroupService _familyGroupService;
        private readonly ApplicationDbContext _applicationDbContext;
        private Mapper mapper;

        public FamilyGroupController()
        {
            _familyGroupService = new FamilyGroupService();
            _applicationDbContext = new ApplicationDbContext();
            var config = AutomapperConfig.instance().Configure();
            mapper = new Mapper(config);
        }

        [HttpGet]
        [Authorize(Roles = "Parent")]
        public IHttpActionResult Get(string id)
        {
            if (id != null)
            {
                var user = _applicationDbContext.Users.Find(id);

                if (user == null)
                {
                    return Content(HttpStatusCode.NotFound, "Parent not found.");
                }
                else
                {
                    if (user.FamilyGroupId == null || user.FamilyGroupId == 0)
                    {
                        return Content(HttpStatusCode.NotFound, "You are not part of a family group.");
                    }
                    else
                    {
                        List <ApplicationUser> familyUsers = _applicationDbContext.Users
                            .Where(x => x.FamilyGroupId == user.FamilyGroupId).ToList();

                        if (familyUsers.Count == 0)
                        {
                            return Content(HttpStatusCode.NotFound, "You don't have any child members of your family.");
                        }
                        else
                        {
                            return Json(mapper.Map<IList<ApplicationUser>, List<ApplicationUserDto>>(familyUsers));
                        }
                    }
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "No ID provided.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post([FromBody] FamilyGroup familyGroup)
        {
            if (familyGroup != null)
            {
                var result = _familyGroupService.CreateAction(familyGroup);

                if (result.Result == true)
                {
                    return Content(HttpStatusCode.OK, "Family Group added successfully.");
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Family Group was not saved, check submitted data.");
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "No family group data was sent.");
            }
        }

    }
}
