using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}