using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using MVCWebAssignment1.Api;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;
using MVCWebAssignment1.ServiceLayer;

namespace MVCWebAssignmentTests.API_Tests
{
    [TestClass]
    public class EventControllerTest
    {

        private Mock<IEventRepository> _mockEventRepository;
        private Mock<IRoundRepository> _mockRoundRepository;
        private Mock<IMeetRepository> _mockMeetRepository;


        public EventControllerTest()
        {
            _mockEventRepository = new Mock<IEventRepository>();
            _mockRoundRepository = new Mock<IRoundRepository>();
            _mockMeetRepository = new Mock<IMeetRepository>();
        }

        [TestMethod]
        public void TestPost()
        {
            _mockMeetRepository.Setup(x => x.GetMeetById(1)).Returns(new Meet());
            EventDto testEventDto = new EventDto
            {
                AgeRange = "Under 16",
                Distance = "100m",
                SwimmingStroke = "Breaststroke"
            };

            var testMeetId = 1;
            var eventController = new EventController(_mockEventRepository.Object, _mockMeetRepository.Object,
                _mockRoundRepository.Object);
            IHttpActionResult actionResult = eventController.Post(testEventDto, testMeetId);
            var result = actionResult as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        }

        [TestMethod]
        public void TestInvalidPost()
        {
            _mockMeetRepository.Setup(x => x.GetMeetById(1)).Returns(new Meet());

            var eventController = new EventController(_mockEventRepository.Object, _mockMeetRepository.Object,
                _mockRoundRepository.Object);
            IHttpActionResult actionResult = eventController.Post(null, 0);
            var result = actionResult as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
