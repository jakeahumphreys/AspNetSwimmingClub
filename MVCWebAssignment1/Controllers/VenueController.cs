using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCWebAssignment1.Customisations;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.Controllers
{
    public class VenueController : Controller
    {
        private IVenueRepository _venueRepository;

        public VenueController()
        {
            _venueRepository = new VenueRepository(new VenueContext());
        }

        public VenueController(IVenueRepository venueRepository)
        {
            this._venueRepository = venueRepository;
        }

        // GET: Venues
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(_venueRepository.GetVenues());
        }

        // GET: Venues/Details/5
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = _venueRepository.GetVenueById(id);

            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // GET: Venues/Create
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                _venueRepository.InsertVenue(venue);
                _venueRepository.Save();
                return RedirectToAction("Index");
            }

            return View(venue);
        }

        // GET: Venues/Edit/5
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = _venueRepository.GetVenueById(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,VenueName,Address")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                _venueRepository.UpdateVenue(venue);
                _venueRepository.Save();
                return RedirectToAction("Index");
            }
            return View(venue);
        }

        // GET: Venues/Delete/5
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Venue venue = _venueRepository.GetVenueById(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Venue venue = _venueRepository.GetVenueById(id);
            _venueRepository.DeleteVenue(venue);
            _venueRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _venueRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
