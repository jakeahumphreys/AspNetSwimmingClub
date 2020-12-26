using Microsoft.AspNet.Identity;
using MVCWebAssignment1.Customisations;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;
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
        private IFamilyGroupRepository _familyGroupRepository;
        private ApplicationDbContext _applicationUserContext;
        // GET: FamilyGroup

        public FamilyGroupController()
        {
            _familyGroupRepository = new FamilyGroupRepository(new FamilyGroupContext());
            _applicationUserContext = new ApplicationDbContext();
        }

        public FamilyGroupController(IFamilyGroupRepository familyGroupRepository)
        {
            this._familyGroupRepository = familyGroupRepository;
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(_familyGroupRepository.GetFamilyGroups());
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            //Create View Model
            FamilyGroupViewModel familyGroupViewModel = new FamilyGroupViewModel();

            //Add object of family group to view model
            familyGroupViewModel.FamilyGroup = _familyGroupRepository.GetFamilyGroupById(id);
            var groupId = familyGroupViewModel.FamilyGroup.FamilyGroupId;

            if (familyGroupViewModel.FamilyGroup == null)
            {
                return HttpNotFound();
            }

            IList<ApplicationUser> familyGroupMembers = new List<ApplicationUser>();

            if(_applicationUserContext != null)
            {
                //Add list of familygroup members to model
                foreach (var user in _applicationUserContext.Users.ToList())
                {
                    if (user.FamilyGroupId > 0)
                    {
                        if (user.FamilyGroupId == groupId)
                        {
                            familyGroupMembers.Add(user);
                        }
                    }
                }

                familyGroupViewModel.FamilyGroupMembers = familyGroupMembers;
            }
          

            return View(familyGroupViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(FamilyGroup familygroup)
        {
            if (ModelState.IsValid)
            {
                _familyGroupRepository.InsertFamilyGroup(familygroup);
                _familyGroupRepository.Save();
                return RedirectToAction("Index");
            }
            return View(familygroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _familyGroupRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}