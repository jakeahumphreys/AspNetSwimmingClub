using Microsoft.AspNet.Identity;
using MVCWebAssignment1.Customisations;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;
using MVCWebAssignment1.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCWebAssignment1.Controllers
{
    public class FamilyGroupController : Controller
    {
        private FamilyGroupService _familyGroupService;


        // GET: FamilyGroup

        public FamilyGroupController()
        {
            _familyGroupService = new FamilyGroupService();
        }

        public FamilyGroupController(IFamilyGroupRepository familyGroupRepository)
        {
            _familyGroupService = new FamilyGroupService(familyGroupRepository);
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(_familyGroupService.GetIndex());
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            try
            {
                return View(_familyGroupService.GetDetails(id));
            }
            catch(ArgumentException ex)
            {
                return RedirectToAction("Error", "Error", new { ErrorType = ErrorType.Service, message = ex.Message });
            }
            catch(HttpException ex)
            {
                return RedirectToAction("Error", "Error", new { ErrorType = ErrorType.Service, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(FamilyGroup familygroup)
        {
            ServiceResponse response = _familyGroupService.CreateAction(familygroup);

            if(response.Result == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _familyGroupService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}