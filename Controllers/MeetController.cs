using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.Controllers
{
    public class MeetController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Meet
        public ActionResult Index()
        {
            return View(db.Meets.ToList());
        }

        // GET: Meet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetViewModel meetViewModel = new MeetViewModel();

            Meet meet = db.Meets.Find(id);

            if (meet == null)
            {
                return HttpNotFound();
            }

            //Get Events for this meet
            List<Event> RelatedEvents = new List<Event>();
            int meetId = meet.Id;
            foreach(var item in db.Events.ToList())
            {
                if(item.Meet != null)
                {
                    if(db.Meets.Find(item.Meet.Id).Id == meetId)
                    {
                        RelatedEvents.Add(item);
                    }       
                }
            }

            //Add components to ViewModel
            meetViewModel.Meet = meet;
            meetViewModel.Events = RelatedEvents;

            //Return View
            return View(meetViewModel);
        }

        // GET: Meet/Create
        public ActionResult Create()
        {
            MeetViewModel meetViewModel = new MeetViewModel();

            meetViewModel.Venues = db.Venues.ToList();

            return View(meetViewModel);
        }

        // POST: Meet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MeetViewModel meetViewModel)
        {
            int venueId;
            int.TryParse(meetViewModel.VenueId, out venueId); //Convert ID from DropDownList to Integer

            //Set ViewModel user's family group to result of a search of family groups by ID
            meetViewModel.Meet.MeetVenue = db.Venues.Where(x => x.Id == venueId).SingleOrDefault();

            if (ModelState.IsValid)
            {
                db.Meets.Add(meetViewModel.Meet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meetViewModel);
        }

        // GET: Meet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MeetViewModel meetViewModel = new MeetViewModel();

            Meet meet = db.Meets.Find(id);
            if (meet == null)
            {
                return HttpNotFound();
            }

            meetViewModel.Meet = meet;
            meetViewModel.Venues = db.Venues.ToList();

            if (meetViewModel.Meet.MeetVenue != null)
            {
                meetViewModel.VenueId = meetViewModel.Meet.MeetVenue.Id.ToString();
            }


            return View(meetViewModel);
        }

        // POST: Meet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MeetViewModel meetViewModel)
        {
            int venueId;
            int.TryParse(meetViewModel.VenueId, out venueId); //Convert ID from DropDownList to Integer

            //Set ViewModel user's family group to result of a search of family groups by ID
            meetViewModel.Meet.MeetVenue = db.Venues.Where(x => x.Id == venueId).SingleOrDefault();

            //load existing meet from database
            Meet meet = db.Meets.Find(meetViewModel.Meet.Id);

            //Set new properties from model
            meet.MeetVenue = meetViewModel.Meet.MeetVenue;

            if (ModelState.IsValid)
            {
                db.Entry(meet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meetViewModel);
        }

        // GET: Meet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meet meet = db.Meets.Find(id);
            if (meet == null)
            {
                return HttpNotFound();
            }
            return View(meet);
        }

        // POST: Meet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meet meet = db.Meets.Find(id);
            foreach(var item in db.Events.ToList())
            {
                if(item.Meet != null)
                {
                    if (item.Meet.Id == id)
                    {
                        db.Events.Remove(item);
                    }
                }
               
            }
            db.Meets.Remove(meet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
