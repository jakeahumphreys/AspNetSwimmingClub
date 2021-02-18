using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using MVCWebAssignment1.Api;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;

namespace MVCWebAssignmentTests.API_Tests
{
    [TestClass]
    public class VenueControllerTest
    {

        private Mock<IVenueRepository> _mockVenueRepository;

        public VenueControllerTest()
        {
            _mockVenueRepository = new Mock<IVenueRepository>();
        }

        [TestMethod]
        public void TestDefaultGet()
        {
            var testVenues = new List<Venue>();
            var testVenue = new Venue();
            testVenues.Add(testVenue);
            _mockVenueRepository.Setup(x => x.GetVenues()).Returns(testVenues);
            var venueController = new VenueController(_mockVenueRepository.Object);
            var result = venueController.Get();
            Assert.AreEqual(typeof(JsonResult<List<VenueDto>>), result.GetType());
        }

        [TestMethod]
        public void TestNoVenuesInDefaultGet()
        {
            var testVenues = new List<Venue>();

            _mockVenueRepository.Setup(x => x.GetVenues()).Returns(testVenues);
            var venueController = new VenueController(_mockVenueRepository.Object);
            IHttpActionResult action = venueController.Get();
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void TestGetSpecificVenue()
        {
            var testVenue = new Venue();

            _mockVenueRepository.Setup(x => x.GetVenueById(1)).Returns(testVenue);
            var venueController = new VenueController(_mockVenueRepository.Object);
            var result = venueController.Get(1);
            Assert.AreEqual(typeof(JsonResult<VenueDto>), result.GetType());
        }

        [TestMethod]
        public void TestGetInvalidSpecificVenueId()
        {
            var testVenue = new Venue();

            _mockVenueRepository.Setup(x => x.GetVenueById(1)).Returns(testVenue);
            var venueController = new VenueController(_mockVenueRepository.Object);
            IHttpActionResult action = venueController.Get(2);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void TestPost()
        {
            var TestVenue = new Venue();
            var venueController = new VenueController(_mockVenueRepository.Object);
            IHttpActionResult action = venueController.Post(TestVenue);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestInvalidPost()
        {
            var TestVenue = new Venue();
            var venueController = new VenueController(_mockVenueRepository.Object);
            IHttpActionResult action = venueController.Post(null);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

    }
}
