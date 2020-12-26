using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using MVCWebAssignment1.Controllers;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace MVCWebAssignmentTests
{
    [TestClass]
    public class EventControllerTest
    {
        private Mock<IEventRepository> _mockEventRepository;
        private Mock<IMeetRepository> _mockMeetRepository;
        private Mock<IRoundRepository> _mockRoundRepository;

        public EventControllerTest()
        {
            _mockEventRepository = new Mock<IEventRepository>();
            _mockMeetRepository = new Mock<IMeetRepository>();
            _mockRoundRepository = new Mock<IRoundRepository>();
        }

        [TestMethod]
        public void EventIndexTest()
        {
            var eventController = new EventController(_mockEventRepository.Object, _mockMeetRepository.Object, _mockRoundRepository.Object);
            var result = eventController.Index();
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void EventCreateViewTest()
        {
            var eventController = new EventController(_mockEventRepository.Object, _mockMeetRepository.Object, _mockRoundRepository.Object);
            var result = eventController.Create(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void EventCreateActionTest()
        {
            var mockEvent = new Event {Id=1, AgeRange="Under 16", Distance="100m", Gender="Any", SwimmingStroke="Stroke", MeetId=1};
            var mockEventVM = new EventViewModel { Event = mockEvent };
            var eventController = new EventController(_mockEventRepository.Object, _mockMeetRepository.Object, _mockRoundRepository.Object);
            var result = eventController.Create(mockEventVM);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void EventDetailsTest()
        {
            var mockEvent = new Event { Id = 1, AgeRange = "Under 16", Distance = "100m", Gender = "Any", SwimmingStroke = "Stroke", MeetId = 1 };
            var mockEventVM = new EventViewModel { Event = mockEvent };
            //Mock Repo setup
            _mockEventRepository.Setup(x => x.GetEventById(1)).Returns(mockEvent);
            _mockRoundRepository.Setup(x => x.GetRounds()).Returns(new List<Round>());
            var eventController = new EventController(_mockEventRepository.Object, _mockMeetRepository.Object, _mockRoundRepository.Object);
            var result = eventController.Details(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void EventEditViewTest()
        {
            var mockEvent = new Event { Id = 1, AgeRange = "Under 16", Distance = "100m", Gender = "Any", SwimmingStroke = "Stroke", MeetId = 1 };
            var mockEventVM = new EventViewModel { Event = mockEvent };
            //Mock Repo setup
            _mockEventRepository.Setup(x => x.GetEventById(1)).Returns(mockEvent);
            var eventController = new EventController(_mockEventRepository.Object, _mockMeetRepository.Object, _mockRoundRepository.Object);
            var result = eventController.Edit(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void EventEditActionTest()
        {
            var mockEvent = new Event { Id = 1, AgeRange = "Under 16", Distance = "100m", Gender = "Any", SwimmingStroke = "Stroke", MeetId = 1 };
            var mockEventVM = new EventViewModel { Event = mockEvent };
            //Mock Repo setup
            _mockEventRepository.Setup(x => x.GetEventById(1)).Returns(mockEvent);
            var eventController = new EventController(_mockEventRepository.Object, _mockMeetRepository.Object, _mockRoundRepository.Object);
            var result = eventController.Edit(mockEvent);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void EventDeleteViewTest()
        {
            var mockEvent = new Event { Id = 1, AgeRange = "Under 16", Distance = "100m", Gender = "Any", SwimmingStroke = "Stroke", MeetId = 1 };
            //Mock Repo setup
            _mockEventRepository.Setup(x => x.GetEventById(1)).Returns(mockEvent);
            var eventController = new EventController(_mockEventRepository.Object, _mockMeetRepository.Object, _mockRoundRepository.Object);
            var result = eventController.Delete(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void EventDeleteActionTest()
        {
            var mockEvent = new Event { Id = 1, AgeRange = "Under 16", Distance = "100m", Gender = "Any", SwimmingStroke = "Stroke", MeetId = 1 };
            //Mock Repo setup
            _mockEventRepository.Setup(x => x.GetEventById(1)).Returns(mockEvent);
            var eventController = new EventController(_mockEventRepository.Object, _mockMeetRepository.Object, _mockRoundRepository.Object);
            var result = eventController.DeleteConfirmed(1);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
        }
    }
}
