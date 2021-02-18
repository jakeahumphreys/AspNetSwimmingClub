using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Http;
using AutoMapper;
using FYP_WebApp.Common_Logic;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;
using MVCWebAssignment1.ServiceLayer;

namespace MVCWebAssignment1.Api
{
    public class RoundController : ApiController
    {
        private readonly RoundService _roundService;
        private readonly EventService _eventService;
        private Mapper _mapper;

        public RoundController()
        {
            _roundService = new RoundService();
            _eventService = new EventService();
            var config = AutomapperConfig.instance().Configure();
            _mapper = new Mapper(config);
        }

        public RoundController(IRoundRepository roundRepository, ILaneRepository laneRepository, IEventRepository eventRepository, IMeetRepository meetRepository)
        {
            _roundService = new RoundService(roundRepository, laneRepository);
            _eventService = new EventService(eventRepository, meetRepository, roundRepository);
            var config = AutomapperConfig.instance().Configure();
            _mapper = new Mapper(config);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post(int eventId)
        {
            if (eventId != 0)
            {
                try
                {
                    var @event = _eventService.GetDetails(eventId);

                    if (@event != null)
                    {
                        var result = _roundService.CreateAction(eventId);

                        if (result.Result == true)
                        {
                            return Content(HttpStatusCode.OK, "Round added to event " + eventId);
                        }
                        else
                        {
                            return Content(HttpStatusCode.BadRequest, "Round not added.");
                        }
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "No event with the specified ID exists.");
                    }
                }
                catch (HttpException)
                {
                    return Content(HttpStatusCode.NotFound, "No event with the specified ID exists.");
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Invalid ID provided.");
            }
        }
    }
}
