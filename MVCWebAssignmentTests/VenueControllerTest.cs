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
        public void IndexTest()
        {
            Mock<IVenueRepository> mockVenueRepository = new Mock<IVenueRepository>();
            var venueController = new VenueController(mockVenueRepository.Object);
            //nulls for search params
            var result = venueController.Index();
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void VenueDetails()
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
    }
}
