using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.Controllers
{
    public class MeetController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private IMeetRepository _meetRepository;
        private IVenueRepository _venueRepository;
        private IEventRepository _eventRepository;

        public MeetController()
        {
            _meetRepository = new MeetRepository(new MeetContext());
            _venueRepository = new VenueRepository(new VenueContext());
            _eventRepository = new EventRepository(new EventContext());
        }

        // GET: Meet
        public ActionResult Index()
        {
            return View(_meetRepository.GetMeets());
        }

        // GET: Meet/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetViewModel meetViewModel = new MeetViewModel();

            Meet meet = _meetRepository.GetMeetById(id);

            if (meet == null)
            {
                return HttpNotFound();
            }

            //Get Events for this meet
            List<Event> RelatedEvents = new List<Event>();
            int meetId = meet.Id;
            foreach (var item in _eventRepository.GetEvents())
            {
                if (item.Meet != null)
                {
                    if (_meetRepository.GetMeetById(item.Meet.Id).Id == meetId)
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

            meetViewModel.Venues = _venueRepository.GetVenues();

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
            meetViewModel.Meet.MeetVenue = _venueRepository.GetVenues().Where(x => x.Id == venueId).SingleOrDefault();

            if (ModelState.IsValid)
            {
                _meetRepository.InsertMeet(meetViewModel.Meet);
                _meetRepository.Save();
                return RedirectToAction("Index");
            }

            return View(meetViewModel);
        }

        // GET: Meet/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MeetViewModel meetViewModel = new MeetViewModel();

            Meet meet = _meetRepository.GetMeetById(id);
            if (meet == null)
            {
                return HttpNotFound();
            }

            meetViewModel.Meet = meet;
            meetViewModel.Venues = _venueRepository.GetVenues();

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
            meetViewModel.Meet.MeetVenue = _venueRepository.GetVenues().Where(x => x.Id == venueId).SingleOrDefault();

            //load existing meet from database
            Meet meet = _meetRepository.GetMeetById(meetViewModel.Meet.Id);

            //Set new properties from model
            meet.MeetVenue = meetViewModel.Meet.MeetVenue;

            if (ModelState.IsValid)
            {
                _meetRepository.UpdateMeet(meet);
                _meetRepository.Save();
                return RedirectToAction("Index");
            }
            return View(meetViewModel);
        }

        // GET: Meet/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meet meet = _meetRepository.GetMeetById(id);
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
            Meet meet = _meetRepository.GetMeetById(id);
            foreach (var item in _eventRepository.GetEvents())
            {
                if (item.Meet != null)
                {
                    if (item.Meet.Id == id)
                    {
                        _eventRepository.DeleteEvent(item);
                    }
                }

            }
            _meetRepository.DeleteMeet(meet);
            _meetRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _meetRepository.Dispose();
                _eventRepository.Dispose();
                _venueRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
