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
    public class MeetControllerTest
    {
        private Mock<IEventRepository> _mockEventRepository;
        private Mock<IVenueRepository> _mockVenueRepository;
        private Mock<IMeetRepository> _mockMeetRepository;

        public MeetControllerTest()
        {
            _mockEventRepository = new Mock<IEventRepository>();
            _mockVenueRepository = new Mock<IVenueRepository>();
            _mockMeetRepository = new Mock<IMeetRepository>();
        }

        [TestMethod]
        public void TestDefaultGet()
        {
            var testMeet = new Meet();
            var testMeetList = new List<Meet>();
            testMeetList.Add(testMeet);

            _mockMeetRepository.Setup(x => x.GetMeets()).Returns(testMeetList);
            var meetController = new MeetController(_mockMeetRepository.Object, _mockEventRepository.Object,
                _mockVenueRepository.Object);
            var result = meetController.Get();
            Assert.AreEqual(typeof(List<MeetDto>), result.GetType());
        }

        [TestMethod]
        public void TestPost()
        {
            _mockVenueRepository.Setup(x => x.GetVenueById(1)).Returns(new Venue());
            var testMeet = new MeetDto
            {
                Date = "01/10/2020",
                MeetName = "TestMeet",
                PoolLength = "100m",
                VenueId = 1
            };
            var meetController = new MeetController(_mockMeetRepository.Object, _mockEventRepository.Object,
                _mockVenueRepository.Object);
            IHttpActionResult action = meetController.Post(testMeet);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestInvalidVenuePost()
        {
            _mockVenueRepository.Setup(x => x.GetVenueById(1)).Returns(new Venue());
            var testMeet = new MeetDto
            {
                Date = "01/10/2020",
                MeetName = "TestMeet",
                PoolLength = "100m",
                VenueId = 0
            };
            var meetController = new MeetController(_mockMeetRepository.Object, _mockEventRepository.Object,
                _mockVenueRepository.Object);
            IHttpActionResult action = meetController.Post(testMeet);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void TestNullDataPost()
        {
            var meetController = new MeetController(_mockMeetRepository.Object, _mockEventRepository.Object,
                _mockVenueRepository.Object);
            IHttpActionResult action = meetController.Post(null);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
