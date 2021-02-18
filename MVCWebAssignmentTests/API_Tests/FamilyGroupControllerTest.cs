using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using MVCWebAssignment1.Api;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;

namespace MVCWebAssignmentTests.API_Tests
{
    [TestClass]
    public class FamilyGroupControllerTest
    {
        private Mock<IFamilyGroupRepository> _mockFamilyGroupRepository;
        private Mock<ApplicationDbContext> _mockApplicationDbContext;

        public FamilyGroupControllerTest()
        {
            _mockFamilyGroupRepository = new Mock<IFamilyGroupRepository>();
            _mockApplicationDbContext = new Mock<ApplicationDbContext>();
        }

        [TestMethod]
        public void TestDefaultGetNoFamilyId()
        {
            var testUser = new ApplicationUser { Id = "testUser"};

            _mockApplicationDbContext.Setup(x => x.Users.Find(1)).Returns(testUser);
            var familyGroupController =
                new FamilyGroupController(_mockFamilyGroupRepository.Object, _mockApplicationDbContext.Object);
            IHttpActionResult action = familyGroupController.Get("testUser");
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void TestDefaultGetNoChildUsers()
        {
            var testUser = new ApplicationUser {Id = "testUser", FamilyGroupId =1};

            _mockApplicationDbContext.Setup(x => x.Users.Find(1)).Returns(testUser);
            var familyGroupController =
                new FamilyGroupController(_mockFamilyGroupRepository.Object, _mockApplicationDbContext.Object);
            IHttpActionResult action = familyGroupController.Get("testUser");
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void TestPost()
        {
            var testFamilyGroup = new FamilyGroup{FamilyGroupId = 1, GroupName = "Test"};
            var familyGroupController =
                new FamilyGroupController(_mockFamilyGroupRepository.Object, _mockApplicationDbContext.Object);
            IHttpActionResult action = familyGroupController.Post(testFamilyGroup);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestPostNullData()
        {
            var familyGroupController =
                new FamilyGroupController(_mockFamilyGroupRepository.Object, _mockApplicationDbContext.Object);
            IHttpActionResult action = familyGroupController.Post(null);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void TestPostBadObject()
        {
            var testFamilyGroup = new FamilyGroup { FamilyGroupId = 1, GroupName = null };
            var familyGroupController =
                new FamilyGroupController(_mockFamilyGroupRepository.Object, _mockApplicationDbContext.Object);
            IHttpActionResult action = familyGroupController.Post(null);
            var result = action as NegotiatedContentResult<string>;
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

    }
}
