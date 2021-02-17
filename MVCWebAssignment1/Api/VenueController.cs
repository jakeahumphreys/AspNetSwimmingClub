using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVCWebAssignment1.ServiceLayer;
using System.Text.Json;
using AutoMapper;
using FYP_WebApp.Common_Logic;
using Microsoft.Ajax.Utilities;
using MVCWebAssignment1.Customisations;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.Api
{
    public class VenueController : ApiController
    {
        private readonly VenueService _venueService;
        private Mapper mapper;

        public VenueController()
        {
            _venueService = new VenueService();
            var config = AutomapperConfig.instance().Configure();
            mapper = new Mapper(config);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get()
        {
            var venues = mapper.Map <IList<Venue>, List<VenueDto>>(_venueService.GetIndex());
            if (venues.Count > 0)
            {
                return Json(venues);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "No venues were found.");
            }
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Json(mapper.Map<Venue, VenueDto>(_venueService.GetDetails(id)));
            }
            catch (ArgumentException)
            {
                return Content(HttpStatusCode.BadRequest, "No ID was provided.");
            }
            catch (VenueNotFoundException)
            {
                return Content(HttpStatusCode.NotFound, "A Venue with the specified ID was not found.");
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Venue venue)
        {
            if (venue != null)
            {
                var result = _venueService.CreateAction(venue);

                if (result.Result == true)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest,
                    "No data was provided to create a venue. Please check your submission.");
            }
        }

    }
}
