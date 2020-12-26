using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using MVCWebAssignment1.Controllers;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;


namespace MVCWebAssignmentTests
{
    [TestClass]
    public class MeetControllerTest
    {

        private Mock<IMeetRepository> _mockMeetRepository;
        private Mock<IVenueRepository> _mockVenueRepository;
        private Mock<IEventRepository> _mockEventRepository;



        public MeetControllerTest()
        {
            _mockMeetRepository = new Mock<IMeetRepository>();
            _mockVenueRepository = new Mock<IVenueRepository>();
            _mockEventRepository = new Mock<IEventRepository>();
        }

        [TestMethod]
        public void MeetIndexTest()
        {
            var meetController = new MeetController(_mockMeetRepository.Object, _mockEventRepository.Object, _mockVenueRepository.Object);
            var result = meetController.Index(null,null);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void MeetCreateViewTest()
        {
            var meetController = new MeetController(_mockMeetRepository.Object, _mockEventRepository.Object, _mockVenueRepository.Object);
            var result = meetController.Create();
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void MeetCreateActionTest()
        {
            var testMeetViewModel = new MeetViewModel();
            var testMeet = new Meet
            {
                Id = 1,
                MeetName = "Test Meet",
                Date = "02/10/1997",
                PoolLength = "100m",
                VenueId = 1
            };
            testMeetViewModel.Meet = testMeet;

            var meetController = new MeetController(_mockMeetRepository.Object, _mockEventRepository.Object, _mockVenueRepository.Object);
            var result = meetController.Create(testMeetViewModel);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void MeetDetailsTest()
        {
            var testMeetViewModel = new MeetViewModel();
            var testMeet = new Meet
            {
                Id = 1,
                MeetName = "Test Meet",
                Date = "02/10/1997",
                PoolLength = "100m",
                VenueId = 1
            };
            testMeetViewModel.Meet = testMeet;

            _mockMeetRepository.Setup(x => x.GetMeetById(1)).Returns(testMeet);
            _mockEventRepository.Setup(x => x.GetEvents()).Returns(new List<Event>());
            var meetController = new MeetController(_mockMeetRepository.Object, _mockEventRepository.Object, _mockVenueRepository.Object);
            var result = meetController.Details(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));

        }

        [TestMethod]
        public void MeetEditViewTest()
        {
            var testMeetViewModel = new MeetViewModel();
            var testMeet = new Meet
            {
                Id = 1,
                MeetName = "Test Meet",
                Date = "02/10/1997",
                PoolLength = "100m",
                VenueId = 1
            };
            testMeetViewModel.Meet = testMeet;

            _mockMeetRepository.Setup(x => x.GetMeetById(1)).Returns(testMeet);
            _mockVenueRepository.Setup(x => x.GetVenues()).Returns(new List<Venue>());
            var meetController = new MeetController(_mockMeetRepository.Object, _mockEventRepository.Object, _mockVenueRepository.Object);
            var result = meetController.Edit(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));

        }

        [TestMethod]
        public void MeetEditActionTest()
        {
            var testMeetViewModel = new MeetViewModel();
            var testMeet = new Meet
            {
                Id = 1,
                MeetName = "Test Meet",
                Date = "02/10/1997",
                PoolLength = "100m",
                VenueId = 1
            };
            testMeetViewModel.Meet = testMeet;

            _mockMeetRepository.Setup(x => x.GetMeetById(1)).Returns(testMeet);
         
            var meetController = new MeetController(_mockMeetRepository.Object, _mockEventRepository.Object, _mockVenueRepository.Object);
            var result = meetController.Edit(testMeetViewModel);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void MeetDeleteViewTest()
        {
            var testMeet = new Meet
            {
                Id = 1,
                MeetName = "Test Meet",
                Date = "02/10/1997",
                PoolLength = "100m",
                VenueId = 1
            };

            _mockMeetRepository.Setup(x => x.GetMeetById(1)).Returns(testMeet);
            var meetController = new MeetController(_mockMeetRepository.Object, _mockEventRepository.Object, _mockVenueRepository.Object);
            var result = meetController.Delete(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));

        }

        [TestMethod]
        public void MeetDeleteActionTest()
        {
            var testMeet = new Meet
            {
                Id = 1,
                MeetName = "Test Meet",
                Date = "02/10/1997",
                PoolLength = "100m",
                VenueId = 1
            };

            _mockMeetRepository.Setup(x => x.GetMeetById(1)).Returns(testMeet);
            _mockEventRepository.Setup(x => x.GetEvents()).Returns(new List<Event>());
            var meetController = new MeetController(_mockMeetRepository.Object, _mockEventRepository.Object, _mockVenueRepository.Object);
            var result = meetController.DeleteConfirmed(1);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
        }
    }
}
