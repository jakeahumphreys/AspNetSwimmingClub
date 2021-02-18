using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using FYP_WebApp.Common_Logic;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;
using MVCWebAssignment1.ServiceLayer;
using Newtonsoft.Json.Linq;

namespace MVCWebAssignment1.Api
{
    public class LaneController : ApiController
    {
        private readonly LaneService _laneService;
        private readonly RoundService _roundService;
        private readonly ApplicationDbContext _applicationDbContext;
        private Mapper _mapper;

        public LaneController()
        {
            _laneService = new LaneService();
            _roundService = new RoundService();
            _applicationDbContext = new ApplicationDbContext();
            var config = AutomapperConfig.instance().Configure();
            _mapper = new Mapper(config);
        }

        public LaneController(ILaneRepository laneRepository, IRoundRepository roundRepository, ApplicationDbContext applicationDbContext)
        {
            _laneService = new LaneService(laneRepository, applicationDbContext);
            _roundService = new RoundService(roundRepository, laneRepository);
            _applicationDbContext = applicationDbContext;
            var config = AutomapperConfig.instance().Configure();
            _mapper = new Mapper(config);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post(int roundId, string swimmerId)
        {
            if (roundId == 0 || string.IsNullOrEmpty(swimmerId))
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data provided, check parameters.");
            }
            else
            {
                try
                {
                    var round = _roundService.GetDetails(roundId);
                    var user = _applicationDbContext.Users.Find(swimmerId);

                    if (round == null)
                    {
                        return Content(HttpStatusCode.NotFound, "No round with the specified id was found.");
                    }
                    else if (user == null)
                    {
                        return Content(HttpStatusCode.NotFound, "No user with the specified id was found.");
                    }
                    else
                    {
                        var laneVm = new LaneViewModel { RoundId = roundId, UserId = swimmerId };
                        var result = _laneService.CreateAction(laneVm);

                        if (result.Result == true)
                        {
                            return Content(HttpStatusCode.OK, "Lane and swimmer added to event successfully.");
                        }
                        else
                        {
                            return Content(HttpStatusCode.InternalServerError,
                                "Lane and swimmer was not added as an error occurred.");
                        }
                    }
                }
                catch (HttpException)
                {
                    return Content(HttpStatusCode.NotFound, "No round with the specified id was found."); 
                }
            }
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Patch([FromBody] LaneDto request)
        {
            if (request == null)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data submitted.");
            }
            else
            {
                try
                {
                    //var existingLane = _laneService.                

                    var existingLane = _laneService.EditView(request.Id);

                    if (existingLane == null)
                    {
                        return Content(HttpStatusCode.NotFound, "The lane with the specified ID was not found.");
                    }
                    else
                    {
                        existingLane.LaneComment = request.LaneComment;
                        existingLane.FinishTime = request.FinishTime;

                        var result = _laneService.EditAction(existingLane);

                        if (result.Result == true)
                        {
                            return Content(HttpStatusCode.OK, "Lane updated successfully.");
                        }
                        else
                        {
                            return Content(HttpStatusCode.InternalServerError,
                                "Lane not updated as the service returned an unsuccessful response.");

                        }
                    }
                }
                catch (HttpException)
                {
                    return Content(HttpStatusCode.NotFound, "The lane with the specified ID was not found.");
                }
            }
        }
    }
}
