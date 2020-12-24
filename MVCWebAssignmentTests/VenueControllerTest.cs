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
        [TestMethod]
        public void VenueIndexTest()
        {
            Mock<IVenueRepository> mockVenueRepository = new Mock<IVenueRepository>();
            var venueController = new VenueController(mockVenueRepository.Object);
            //nulls for search params
            var result = venueController.Index();
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void VenueDetailsTest()
        {
            var testVenue = new Venue {
                Id = 1,
                VenueName = "Test Venue",
                Address = "Test Street"
            };

            Mock<IVenueRepository> mockVenueRepository = new Mock<IVenueRepository>();
            mockVenueRepository.Setup(x => x.GetVenueById(1)).Returns(testVenue);
            var venueController = new VenueController(mockVenueRepository.Object);
            var result = venueController.Details(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void VenueCreateTest()
        {
            var testVenue = new Venue
            {
                Id = 1,
                VenueName = "Test Venue",
                Address = "Test Street"
            };

            Mock<IVenueRepository> mockVenueRepository = new Mock<IVenueRepository>();
            var venueController = new VenueController(mockVenueRepository.Object);
            var result = venueController.Create(testVenue);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));

        }

        [TestMethod]
        public void VenueEditTest()
        {
            var testVenue = new Venue
            {
                Id = 1,
                VenueName = "Test Venue",
                Address = "Test Street"
            };

            Mock<IVenueRepository> mockVenueRepository = new Mock<IVenueRepository>();
            var venueController = new VenueController(mockVenueRepository.Object);
            mockVenueRepository.Setup(x => x.UpdateVenue(It.IsAny<Venue>()));
            var result = venueController.Edit(testVenue);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));

        }

        [TestMethod]
        public void VenueDeleteTest()
        {
            var testVenue = new Venue
            {
                Id = 1,
                VenueName = "Test Venue",
                Address = "Test Street"
            };

            Mock<IVenueRepository> mockVenueRepository = new Mock<IVenueRepository>();
            var venueController = new VenueController(mockVenueRepository.Object);
            mockVenueRepository.Setup(x => x.GetVenueById(1)).Returns(testVenue);
            var result = venueController.Delete(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void VenueDeleteConfirmedTest()
        {
            var testVenue = new Venue
            {
                Id = 1,
                VenueName = "Test Venue",
                Address = "Test Street"
            };

            Mock<IVenueRepository> mockVenueRepository = new Mock<IVenueRepository>();
            var venueController = new VenueController(mockVenueRepository.Object);
            mockVenueRepository.Setup(x => x.DeleteVenue(It.IsAny<Venue>()));
            var result = venueController.DeleteConfirmed(1);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
        }
    }
}
