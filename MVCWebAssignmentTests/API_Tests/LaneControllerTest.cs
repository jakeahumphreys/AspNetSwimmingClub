using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
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
    public class LaneControllerTest
    {
        private Mock<ILaneRepository> _mockLaneRepository;
        private Mock<IRoundRepository> _mockRoundRepository;
        private Mock<ApplicationDbContext> _mockAppDbContext;


        public LaneControllerTest()
        {
            _mockLaneRepository = new Mock<ILaneRepository>();
            _mockRoundRepository = new Mock<IRoundRepository>();
            _mockAppDbContext = new Mock<ApplicationDbContext>();
        }

        [TestMethod]
        public void TestPost()
        {
            var testApplicationUser = new ApplicationUser
            {
                Id="testId",
                Name = "Test User"
            };

            var testRound = new Round
            {
                EventId = 1,
                RoundNumber = 1
            };

            var testLane = new Lane
            {
                Id = 1,
                LaneNumber = 1,
                RoundId = 1,
                SwimmerId = "testId"
            };

            _mockAppDbContext.Setup(x => x.Users.Find("testId")).Returns(testApplicationUser);
            _mockRoundRepository.Setup(x => x.GetRoundById(1)).Returns(testRound);
            _mockLaneRepository.Setup(x => x.GetLanes()).Returns(new List<Lane>());
            var laneController = new LaneController(_mockLaneRepository.Object, _mockRoundRepository.Object,
                _mockAppDbContext.Object);
            IHttpActionResult action = laneController.Post(1, "testId");
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestInvalidPostNullRound()
        {
            var testApplicationUser = new ApplicationUser
            {
                Id = "testId",
                Name = "Test User"
            };

            var testRound = new Round
            {
                EventId = 1,
                RoundNumber = 1
            };

            _mockAppDbContext.Setup(x => x.Users.Find("testId")).Returns(testApplicationUser);
            _mockRoundRepository.Setup(x => x.GetRoundById(1)).Returns(testRound);
            _mockLaneRepository.Setup(x => x.GetLanes()).Returns(new List<Lane>());
            var laneController = new LaneController(_mockLaneRepository.Object, _mockRoundRepository.Object,
                _mockAppDbContext.Object);
            IHttpActionResult action = laneController.Post(2, "testId");
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void TestInvalidPostNonExistingUser()
        {
            var testApplicationUser = new ApplicationUser
            {
                Id = "testId",
                Name = "Test User"
            };

            var testRound = new Round
            {
                EventId = 1,
                RoundNumber = 1
            };

            _mockAppDbContext.Setup(x => x.Users.Find("testId")).Returns(testApplicationUser);
            _mockRoundRepository.Setup(x => x.GetRoundById(1)).Returns(testRound);
            _mockLaneRepository.Setup(x => x.GetLanes()).Returns(new List<Lane>());
            var laneController = new LaneController(_mockLaneRepository.Object, _mockRoundRepository.Object,
                _mockAppDbContext.Object);
            IHttpActionResult action = laneController.Post(2, "invalidUser");
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void TestInvalidPostInvalidRoundId()
        {
            var testApplicationUser = new ApplicationUser
            {
                Id = "testId",
                Name = "Test User"
            };

            var testRound = new Round
            {
                EventId = 1,
                RoundNumber = 1
            };

            _mockAppDbContext.Setup(x => x.Users.Find("testId")).Returns(testApplicationUser);
            _mockRoundRepository.Setup(x => x.GetRoundById(1)).Returns(testRound);
            _mockLaneRepository.Setup(x => x.GetLanes()).Returns(new List<Lane>());
            var laneController = new LaneController(_mockLaneRepository.Object, _mockRoundRepository.Object,
                _mockAppDbContext.Object);
            IHttpActionResult action = laneController.Post(0, "invalidUser");
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void TestInvalidPostInvalidUserId()
        {
            var testApplicationUser = new ApplicationUser
            {
                Id = "testId",
                Name = "Test User"
            };

            var testRound = new Round
            {
                EventId = 1,
                RoundNumber = 1
            };

            _mockAppDbContext.Setup(x => x.Users.Find("testId")).Returns(testApplicationUser);
            _mockRoundRepository.Setup(x => x.GetRoundById(1)).Returns(testRound);
            _mockLaneRepository.Setup(x => x.GetLanes()).Returns(new List<Lane>());
            var laneController = new LaneController(_mockLaneRepository.Object, _mockRoundRepository.Object,
                _mockAppDbContext.Object);
            IHttpActionResult action = laneController.Post(1, null);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void TestPatch()
        {
            var testLane = new Lane
            {
                Id = 1,
                LaneNumber = 1,
                RoundId = 1,
                SwimmerId = "testId"
            };

            var testLaneDto = new LaneDto
            {
                Id=1,
                FinishTime = "2min",
                LaneComment = "Test Comment"
            };


            _mockLaneRepository.Setup(x => x.GetLaneById(1)).Returns(testLane);
            var laneController = new LaneController(_mockLaneRepository.Object, _mockRoundRepository.Object,
                _mockAppDbContext.Object);
            IHttpActionResult action = laneController.Patch(testLaneDto);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        }

        [TestMethod]
        public void TestPatchInvalidLane()
        {
            var testLane = new Lane
            {
                Id = 1,
                LaneNumber = 1,
                RoundId = 1,
                SwimmerId = "testId"
            };

            var testLaneDto = new LaneDto
            {
                Id = 2,
                FinishTime = "2min",
                LaneComment = "Test Comment"
            };


            _mockLaneRepository.Setup(x => x.GetLaneById(1)).Returns(testLane);
            var laneController = new LaneController(_mockLaneRepository.Object, _mockRoundRepository.Object,
                _mockAppDbContext.Object);
            IHttpActionResult action = laneController.Patch(testLaneDto);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);

        }

    }
}
