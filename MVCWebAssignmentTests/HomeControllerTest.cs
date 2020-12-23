using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MVCWebAssignment1.Controllers;
using System.Web.Mvc;

namespace MVCWebAssignmentTests
{
    [TestClass]
    public class HomeTest
    {
        [TestMethod]
        public void HomeReturnsViewResult()
        {
            var homeController = new HomeController();
            //nulls for search params
            var result = homeController.Index(null,null,null,null,null);
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }
    }
}
