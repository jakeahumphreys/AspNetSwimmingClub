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
    public class FamilyGroupControllerTest
    {
        private Mock<IFamilyGroupRepository> _mockFamilyGroupRepository;

        public FamilyGroupControllerTest()
        {
            _mockFamilyGroupRepository = new Mock<IFamilyGroupRepository>();
        }

        [TestMethod]
        public void FamilyGroupIndexTest()
        {
            _mockFamilyGroupRepository.Setup(x => x.GetFamilyGroups()).Returns(new List<FamilyGroup>());
            var familyGroupController = new FamilyGroupController(_mockFamilyGroupRepository.Object);
            var result = familyGroupController.Index();
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        [TestMethod]
        public void FamilyGroupDetailsTest()
        {

            var testFamilyGroup = new FamilyGroup { FamilyGroupId = 1, GroupName = "Test Group"};

            //Mock family repository
            _mockFamilyGroupRepository.Setup(x => x.GetFamilyGroupById(1)).Returns(testFamilyGroup);

            var familyGroupController = new FamilyGroupController(_mockFamilyGroupRepository.Object);
            var result = familyGroupController.Details(1);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));

        }

        [TestMethod]
        public void FamilyGroupCreateViewTest()
        {
            //Mock family repository
            var familyGroupController = new FamilyGroupController(_mockFamilyGroupRepository.Object);
            var result = familyGroupController.Create();
            Assert.AreEqual(result.GetType(), typeof(ViewResult));

        }

        [TestMethod]
        public void FamilyGroupCreateActionTest()
        {
            var testFamilyGroup = new FamilyGroup { FamilyGroupId = 1, GroupName = "Test Group" };

            //Mock family repository
            var familyGroupController = new FamilyGroupController(_mockFamilyGroupRepository.Object);
            var result = familyGroupController.Create(testFamilyGroup);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));

        }

    }
}
