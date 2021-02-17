using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using AutoMapper;
using FYP_WebApp.Common_Logic;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;
using MVCWebAssignment1.ServiceLayer;

namespace MVCWebAssignment1.Api
{
    public class EventController : ApiController
    {
        private readonly EventService _eventService;
        private Mapper _mapper;

        public EventController()
        {
            _eventService = new EventService();
            var config = AutomapperConfig.instance().Configure();
            _mapper = new Mapper(config);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post([FromBody] EventDto request, int meetId)
        {
            if (request != null)
            {
                var @event = _mapper.Map<EventDto, Event>(request);
                var eventVM = new EventViewModel {Event = @event, MeetId = meetId};
                var Result = _eventService.CreateAction(eventVM);

                if (Result.Result == true)
                {
                    return Content(HttpStatusCode.OK, "Event Added successfully");
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Event not added.");
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "No data submitted.");
            }
        }
       
    }
}