using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using MVCWebAssignment1.Api;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;

namespace MVCWebAssignmentTests.API_Tests
{
    [TestClass]
    public class RoundControllerTest
    {
        private Mock<IRoundRepository> _mockRoundRepository;
        private Mock<ILaneRepository> _mockLaneRepository;
        private Mock<IEventRepository> _mockEventRepository;
        private Mock<IMeetRepository> _mockMeetRepository;

        public RoundControllerTest()
        {
            _mockRoundRepository = new Mock<IRoundRepository>();
            _mockLaneRepository = new Mock<ILaneRepository>();
            _mockEventRepository = new Mock<IEventRepository>();
            _mockMeetRepository = new Mock<IMeetRepository>();
        }

        [TestMethod]
        public void TestPost()
        {
            var testEvent = new Event {Id = 1, AgeRange = "under16", Gender = "Male", Distance = "100m"};

            _mockEventRepository.Setup(x => x.GetEventById(1)).Returns(testEvent);
            _mockRoundRepository.Setup(x => x.GetRounds()).Returns(new List<Round>());
            var roundController = new RoundController(_mockRoundRepository.Object, _mockLaneRepository.Object,
                _mockEventRepository.Object, _mockMeetRepository.Object);
            IHttpActionResult action = roundController.Post(1);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestPostInvalidEventId()
        {
            var testEvent = new Event { Id = 1, AgeRange = "under16", Gender = "Male", Distance = "100m" };

            _mockEventRepository.Setup(x => x.GetEventById(1)).Returns(testEvent);
            _mockRoundRepository.Setup(x => x.GetRounds()).Returns(new List<Round>());
            var roundController = new RoundController(_mockRoundRepository.Object, _mockLaneRepository.Object,
                _mockEventRepository.Object, _mockMeetRepository.Object);
            IHttpActionResult action = roundController.Post(0);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void TestPostNonExistingRoundId()
        {
            var testEvent = new Event { Id = 1, AgeRange = "under16", Gender = "Male", Distance = "100m" };

            _mockEventRepository.Setup(x => x.GetEventById(1)).Returns(testEvent);
            _mockRoundRepository.Setup(x => x.GetRounds()).Returns(new List<Round>());
            var roundController = new RoundController(_mockRoundRepository.Object, _mockLaneRepository.Object,
                _mockEventRepository.Object, _mockMeetRepository.Object);
            IHttpActionResult action = roundController.Post(3);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

    }
}
