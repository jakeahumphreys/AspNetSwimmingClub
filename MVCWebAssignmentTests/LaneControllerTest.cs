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
    public class LaneControllerTest
    {
        private Mock<ILaneRepository> _mockLaneRepository;
        private Mock<ApplicationDbContext> _mockApplicationDbContext;

        public LaneControllerTest()
        {
            _mockLaneRepository = new Mock<ILaneRepository>();
            _mockApplicationDbContext = new Mock<ApplicationDbContext>();
        }

        [TestMethod]
        public void LaneIndexTest()
        {
            var laneController = new LaneController(_mockLaneRepository.Object, _mockApplicationDbContext.Object);
            var result = laneController.Index();
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void LaneCreateViewTest()
        {
            var mockLane = new Lane { Id = 1, LaneNumber = 1, RoundId = 1, FinishTime = "", LaneComment = "", SwimmerId = "1" };

            _mockLaneRepository.Setup(x => x.GetLaneById(1)).Returns(mockLane);
            var laneController = new LaneController(_mockLaneRepository.Object, null);
            var result = laneController.Create(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        public void LaneCreateActionTest()
        {
            var mockLane = new Lane { Id = 1, LaneNumber = 1, RoundId = 1, FinishTime = "", LaneComment = "", SwimmerId = "1" };
            var mockLaneVM = new LaneViewModel { Lane = mockLane };
            var laneController = new LaneController(_mockLaneRepository.Object, null);
            var result = laneController.Create(mockLaneVM);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void LaneEditViewTest()
        {
            var mockLane = new Lane {Id = 1, LaneNumber = 1, RoundId = 1, FinishTime = "", LaneComment = "", SwimmerId="1"};

            _mockLaneRepository.Setup(x => x.GetLaneById(1)).Returns(mockLane);
            var laneController = new LaneController(_mockLaneRepository.Object, _mockApplicationDbContext.Object);
            var result = laneController.Edit(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void LaneEditActionTest()
        {
            var mockLane = new Lane { Id = 1, LaneNumber = 1, RoundId = 1, FinishTime = "", LaneComment = "", SwimmerId = "1" };

            _mockLaneRepository.Setup(x => x.GetLaneById(1)).Returns(mockLane);
            var laneController = new LaneController(_mockLaneRepository.Object, _mockApplicationDbContext.Object);
            var result = laneController.Edit(mockLane);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void LaneDeleteViewTest()
        {
            var mockLane = new Lane { Id = 1, LaneNumber = 1, RoundId = 1, FinishTime = "", LaneComment = "", SwimmerId = "1" };
            _mockLaneRepository.Setup(x => x.GetLaneById(1)).Returns(mockLane);
            var laneController = new LaneController(_mockLaneRepository.Object, _mockApplicationDbContext.Object);
            var result = laneController.Delete(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void LaneDeleteActionTest()
        {
            var mockLane = new Lane { Id = 1, LaneNumber = 1, RoundId = 1, FinishTime = "", LaneComment = "", SwimmerId = "1" };
            _mockLaneRepository.Setup(x => x.GetLaneById(1)).Returns(mockLane);
            _mockLaneRepository.Setup(x => x.GetLanes()).Returns(new List<Lane>());
            var laneController = new LaneController(_mockLaneRepository.Object, _mockApplicationDbContext.Object);
            var result = laneController.DeleteConfirmed(1);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
        }
    }
}
