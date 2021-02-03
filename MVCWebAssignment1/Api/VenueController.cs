using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVCWebAssignment1.ServiceLayer;
using System.Text.Json;
using Microsoft.Ajax.Utilities;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.Api
{
    public class VenueController : ApiController
    {
        private readonly VenueService _venueService;

        public VenueController()
        {
            _venueService = new VenueService();
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get()
        {
            
            var venues = _venueService.GetIndex();
            if (venues.Count > 0)
            {
                return Json(venues);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "No venues were found.");
            }
        }

        [Route("GetById")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var venue = _venueService.GetDetails(id);

            if (venue != null)
            {
                return Json(venue);
            }
            else
            {
                return NotFound();
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
                return BadRequest();
            }
        }

    }
}
