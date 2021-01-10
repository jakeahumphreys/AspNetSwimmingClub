using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWebAssignment1.Customisations;

namespace MVCWebAssignment1.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Unauthorized
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error(ErrorType errorType, string message)
        {
            ViewBag.ErrorType = errorType;
            ViewBag.Message = message ?? "An error has occurred.";
            return View();
        }
    }
}