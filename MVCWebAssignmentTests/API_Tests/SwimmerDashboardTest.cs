using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Results;
using Moq;
using MVCWebAssignment1.Api;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;
using System.Data.Entity;

namespace MVCWebAssignmentTests.API_Tests
{
    [TestClass]
    public class SwimmerDashboardTest
    {

        private Mock<ILaneRepository> _mockLaneRepository;
        private Mock<IRoundRepository> _mockRoundRepository;
        private Mock<IMeetRepository> _mockMeetRepository;
        private Mock<IEventRepository> _mockEventRepository;
        private Mock<ApplicationDbContext> _mockApplicationDbContext;

        public SwimmerDashboardTest()
        {
            _mockLaneRepository = new Mock<ILaneRepository>();
            _mockRoundRepository = new Mock<IRoundRepository>();
            _mockMeetRepository = new Mock<IMeetRepository>();
            _mockEventRepository = new Mock<IEventRepository>();
            _mockApplicationDbContext = new Mock<ApplicationDbContext>();
        }

        [TestMethod]
        public void SwimmerDashboardPersonalTest()
        {
            _mockLaneRepository.Setup(x => x.GetLanes()).Returns(new List<Lane>());
            var swimmerDashboardController = new SwimmerDashboardController(_mockApplicationDbContext.Object, _mockLaneRepository.Object, _mockEventRepository.Object, _mockMeetRepository.Object, _mockRoundRepository.Object);
            var result = swimmerDashboardController.Get(null);
            Assert.AreEqual(result.GetType(), typeof(NegotiatedContentResult<string>));
        }

    }
}
