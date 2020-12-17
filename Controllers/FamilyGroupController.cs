using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCWebAssignment1.Controllers
{
    public class FamilyGroupController : Controller
    {
        private ApplicationDbContext _context;
        // GET: FamilyGroup

        public FamilyGroupController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View(_context.FamilyGroups.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FamilyGroup familyGroup = _context.FamilyGroups.Find(id);

            if (familyGroup == null)
            {
                return HttpNotFound();
            }
            return View(familyGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id, Name, Parent, Children")] FamilyGroup familygroup)
        {
            if (ModelState.IsValid)
            {
                _context.FamilyGroups.Add(familygroup);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(familygroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}