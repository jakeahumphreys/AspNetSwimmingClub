using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;
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

        [HttpGet]
        public IEnumerable<MeetDto> Get()
        {
            return mapper.Map<IList<Meet>, List<MeetDto>>(_meetService.GetIndex(null,null));
        }

        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Meet
        public IHttpActionResult Post([FromBody]MeetDto request)
        {
            if (!string.IsNullOrEmpty(request.ToString()))
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

        // PUT: api/Meet/5
        public IHttpActionResult Put(int id, [FromBody]MeetDto request)
        {
            return BadRequest();
        }

        // DELETE: api/Meet/5
        public void Delete(int id)
        {
        }
    }
}
