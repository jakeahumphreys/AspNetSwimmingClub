using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MVCWebAssignment1.Controllers;
using System.Web.Mvc;
using Moq;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;

namespace MVCWebAssignmentTests
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
        public void VenueIndexTest()
        {
            var venueController = new VenueController(_mockVenueRepository.Object);
            //nulls for search params
            var result = venueController.Index();
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void VenueCreateViewTest()
        {
            var venueController = new VenueController(_mockVenueRepository.Object);
            var result = venueController.Create();
            Assert.AreEqual(result.GetType(), typeof(ViewResult));

        }

        [TestMethod]
        public void VenueCreateActionTest()
        {
            var mockVenue = new Venue {Id = 1, VenueName = "Test Venue", Address = "Test Street"};

            var venueController = new VenueController(_mockVenueRepository.Object);
            var result = venueController.Create(mockVenue);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));

        }

        [TestMethod]
        public void VenueDetailsTest()
        {
            var mockVenue = new Venue { Id = 1, VenueName = "Test Venue", Address = "Test Street" };

            _mockVenueRepository.Setup(x => x.GetVenueById(1)).Returns(mockVenue);
            var venueController = new VenueController(_mockVenueRepository.Object);
            var result = venueController.Details(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }


        [TestMethod]
        public void VenueEditViewTest()
        {
            var mockVenue = new Venue { Id = 1, VenueName = "Test Venue", Address = "Test Street" };


            var venueController = new VenueController(_mockVenueRepository.Object);
            _mockVenueRepository.Setup(x => x.GetVenueById(1)).Returns(mockVenue);
            var result = venueController.Edit(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));

        }

        [TestMethod]
        public void VenueEditActionTest()
        {
            var mockVenue = new Venue { Id = 1, VenueName = "Test Venue", Address = "Test Street" };


            var venueController = new VenueController(_mockVenueRepository.Object);
            _mockVenueRepository.Setup(x => x.GetVenueById(1)).Returns(mockVenue);
            var result = venueController.Edit(mockVenue);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));

        }

        [TestMethod]
        public void VenueDeleteTest()
        {
            var mockVenue = new Venue { Id = 1, VenueName = "Test Venue", Address = "Test Street" };


            var venueController = new VenueController(_mockVenueRepository.Object);
            _mockVenueRepository.Setup(x => x.GetVenueById(1)).Returns(mockVenue);
            var result = venueController.Delete(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void VenueDeleteConfirmedTest()
        {
            var mockVenue = new Venue { Id = 1, VenueName = "Test Venue", Address = "Test Street" };


            var venueController = new VenueController(_mockVenueRepository.Object);
            _mockVenueRepository.Setup(x => x.DeleteVenue(It.IsAny<Venue>()));
            var result = venueController.DeleteConfirmed(1);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
        }
    }
}
