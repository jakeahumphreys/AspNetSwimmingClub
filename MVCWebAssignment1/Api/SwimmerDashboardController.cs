using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FYP_WebApp.Common_Logic;
using Microsoft.AspNet.Identity;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;
using MVCWebAssignment1.ServiceLayer;

namespace MVCWebAssignment1.Api
{
    public class SwimmerDashboardController : ApiController
    {
        private readonly MeetService _meetService;
        private readonly EventService _eventService;
        private readonly LaneService _laneService;
        private readonly FamilyGroupService _familyGroupService;
        private readonly DashboardService _dashboardService;
        private Mapper _mapper;

        public SwimmerDashboardController()
        {
            _meetService = new MeetService();
            _eventService = new EventService();
            _familyGroupService = new FamilyGroupService();
            _laneService = new LaneService();
            _dashboardService = new DashboardService();
            var config = AutomapperConfig.instance().Configure();
            _mapper = new Mapper(config);
        }

        public SwimmerDashboardController(ApplicationDbContext applicationDbContext, ILaneRepository laneRepository, IEventRepository eventRepository, IMeetRepository meetRepository, IRoundRepository roundRepository)
        {
            _laneService = new LaneService(laneRepository, applicationDbContext);
            _dashboardService =
                new DashboardService(applicationDbContext, eventRepository, meetRepository, roundRepository);
            var config = AutomapperConfig.instance().Configure();
            _mapper = new Mapper(config);
        }

        [Route("api/SwimmerDashboard/Personal")]
        [HttpGet]
        [Authorize(Roles = "Member,Admin")]
        public IHttpActionResult Get(string swimmerId)
        {
            if (string.IsNullOrEmpty(swimmerId))
            {
                return Content(HttpStatusCode.BadRequest, "Invalid ID provided.");
            }
            else
            {
                var requestingUserId = User.Identity.GetUserId();
                

                if (swimmerId == requestingUserId || User.IsInRole("Admin"))
                {
                    var personalLanes = _mapper.Map<IList<Lane>, List<LaneDto>>(_laneService.GetIndex().Where(x => x.SwimmerId == swimmerId).ToList());
                    return Json(personalLanes);
                   
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, "You are unable to view that users personal dashboard.");
                }
            }
        }

        [Route("api/SwimmerDashboard/Search/Name")]
        [HttpGet]
        public IHttpActionResult SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Content(HttpStatusCode.BadRequest, "No name specified");
            }
            else
            {
                var users = _mapper.Map<IList<ApplicationUser>, List<ApplicationUserDto>>(
                    _dashboardService.SearchUserByName(name));
                return Json(users);
            }
        }

        [Route("api/SwimmerDashboard/Search/Stroke")]
        [HttpGet]
        public IHttpActionResult SearchByStroke(string stroke)
        {
            if (string.IsNullOrEmpty(stroke))
            {
                return Content(HttpStatusCode.BadRequest, "No stroke specified.");
            }
            else
            {
                var users = _mapper.Map<IList<ApplicationUser>, List<ApplicationUserDto>>(
                    _dashboardService.SearchUserByStroke(stroke));
                return Json(users);
            }
        }

        [Route("api/SwimmerDashboard/Search/Age")]
        [HttpGet]
        public IHttpActionResult SearchByAge(int age)
        {
            if (age == 0)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid age provided");
            }
            else
            {
                var users =
                    _mapper.Map<IList<ApplicationUser>, List<ApplicationUserDto>>(_dashboardService.SearchUserByAge(age));
                return Json(users);
            }
        }
    }
}
