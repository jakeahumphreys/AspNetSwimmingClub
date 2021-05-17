using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using AutoMapper;
using FYP_WebApp.Common_Logic;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;
using MVCWebAssignment1.ServiceLayer;

namespace MVCWebAssignment1.Api
{
    public class MeetController : ApiController
    {
        private readonly MeetService _meetService;
        private readonly Mapper mapper;

        public MeetController()
        {
            _meetService = new MeetService();
            var config = AutomapperConfig.instance().Configure();
            mapper = new Mapper(config);
        }

        public MeetController(IMeetRepository meetRepository, IEventRepository eventRepository, IVenueRepository venueRepository)
        {
            _meetService = new MeetService(meetRepository, eventRepository, venueRepository);
            var config = AutomapperConfig.instance().Configure();
            mapper = new Mapper(config);
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<MeetDto> Get()
        {
            return mapper.Map<IList<Meet>, List<MeetDto>>(_meetService.GetIndex(null,null));
        }

        [System.Web.Http.HttpGet]
        public JsonResult<MeetDto> Get(int id)
        {
            return Json(mapper.Map<Meet, MeetDto>(_meetService.GetMeetDetails(id)));
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Post([FromBody]MeetDto request)
        {
            if (request != null)
            {
                var meet = mapper.Map<MeetDto, Meet>(request);
                var meetVm = new MeetViewModel {Meet = meet, VenueId = meet.VenueId.ToString()};
                
                var Result = _meetService.CreateAction(meetVm);

                if (Result.Result == true)
                {
                    return Content(HttpStatusCode.OK, "Meet added successfully");
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Meet failed to add.");
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data submitted.");
            }
        }
    }
}
