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
    public class RoundControllerTest
    {
        private Mock<IRoundRepository> _mockRoundRepository;
        private Mock<ILaneRepository> _mockLaneRepository;

        public RoundControllerTest()
        {
            _mockLaneRepository = new Mock<ILaneRepository>();
            _mockRoundRepository = new Mock<IRoundRepository>();
        }

        [TestMethod]
        public void RoundIndexTest()
        {
            _mockRoundRepository.Setup(x => x.GetRounds()).Returns(new List<Round>());
            var roundController = new RoundController(_mockRoundRepository.Object, _mockLaneRepository.Object);
            var result = roundController.Index();
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }


        [TestMethod]
        public void RoundCreateActionTest()
        {
            var mockRound = new Round { Id = 1, RoundNumber = 1 };
            _mockRoundRepository.Setup(x => x.GetRoundById(1)).Returns(mockRound);
            _mockRoundRepository.Setup(x => x.GetRounds()).Returns(new List<Round>());
            var roundController = new RoundController(_mockRoundRepository.Object, _mockLaneRepository.Object);
            var result = roundController.Create(1);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RoundDetailsTest()
        {
            var mockRound = new Round { Id = 1, RoundNumber = 1 };
            var mockRoundVM = new RoundViewModel { Round = mockRound };
            _mockRoundRepository.Setup(x => x.GetRoundById(1)).Returns(mockRound);
            _mockLaneRepository.Setup(x => x.GetLanes()).Returns(new List<Lane>());
            var roundController = new RoundController(_mockRoundRepository.Object, _mockLaneRepository.Object);
            var result = roundController.Details(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void RoundDeleteViewTest()
        {
            var mockRound = new Round { Id = 1, RoundNumber = 1 };
            _mockRoundRepository.Setup(x => x.GetRoundById(1)).Returns(mockRound);
            var roundController = new RoundController(_mockRoundRepository.Object, _mockLaneRepository.Object);
            var result = roundController.Delete(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void RoundDeleteActionTest()
        {
            var mockRound = new Round { Id = 1, RoundNumber = 1 };
            _mockRoundRepository.Setup(x => x.GetRoundById(1)).Returns(mockRound);
            _mockRoundRepository.Setup(x => x.GetRounds()).Returns(new List<Round>());
            var roundController = new RoundController(_mockRoundRepository.Object, _mockLaneRepository.Object);
            var result = roundController.DeleteConfirmed(1);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
        }



    }
}
