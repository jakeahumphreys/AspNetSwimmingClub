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

        public MeetController(IMeetRepository meetRepository, IEventRepository eventRepository, IVenueRepository venueRepository)
        {
            this._meetRepository = meetRepository;
            this._eventRepository = eventRepository;
            this._venueRepository = venueRepository;
        }

        // GET: Meet
        public ActionResult Index(string searchParamDateLower, string searchParamDateUpper)
        {
            IList<Meet> meets = _meetRepository.GetMeets();

            if(!String.IsNullOrEmpty(searchParamDateLower) && !String.IsNullOrEmpty(searchParamDateUpper))
            {
                IList<Meet> updatedMeets = new List<Meet>();
                DateTime startDate = Convert.ToDateTime(searchParamDateLower);
                DateTime endDate = Convert.ToDateTime(searchParamDateUpper);

                foreach (var meet in meets)
                {
                    DateTime convertedDate = Convert.ToDateTime(meet.Date);
                   
                    if(convertedDate >= startDate && convertedDate <= endDate)
                    {
                        updatedMeets.Add(meet);
                    }
                    
                }

                meets = updatedMeets;
            }

            return View(meets);
        }

        // GET: Meet/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
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
            foreach (var @event in _eventRepository.GetEvents())
            {
                if (@event.MeetId != 0)
                {
                    if(@event.MeetId == meet.Id)
                    {
                        RelatedEvents.Add(@event);
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
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create()
        {
            MeetViewModel meetViewModel = new MeetViewModel();

            if(_venueRepository != null)
            {
                meetViewModel.Venues = _venueRepository.GetVenues();
            }


            return View(meetViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(MeetViewModel meetViewModel)
        {
            int venueId;
            int.TryParse(meetViewModel.VenueId, out venueId); //Convert ID from DropDownList to Integer

            //Set ViewModel user's family group to result of a search of family groups by ID
            //meetViewModel.Meet.Venue = _venueRepository.GetVenues().Where(x => x.Id == venueId).SingleOrDefault();
            meetViewModel.Meet.VenueId = venueId;

            if (ModelState.IsValid)
            {
                _meetRepository.InsertMeet(meetViewModel.Meet);
                _meetRepository.Save();
                return RedirectToAction("Index");
            }

            return View(meetViewModel);
        }

        // GET: Meet/Edit/5
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
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

            if (meetViewModel.Meet.VenueId != 0)
            {
                meetViewModel.VenueId = meetViewModel.Meet.VenueId.ToString();
            }


            return View(meetViewModel);
        }

        // POST: Meet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(MeetViewModel meetViewModel)
        {
            int venueId;
            int.TryParse(meetViewModel.VenueId, out venueId); //Convert ID from DropDownList to Integer

            //Set ViewModel user's family group to result of a search of family groups by ID
            //meetViewModel.Meet.Venue = _venueRepository.GetVenues().Where(x => x.Id == venueId).SingleOrDefault();
            meetViewModel.Meet.VenueId = venueId;


            //load existing meet from database
            Meet meet = _meetRepository.GetMeetById(meetViewModel.Meet.Id);

            //Set new properties from model
            meet.VenueId= meetViewModel.Meet.VenueId;
            meet.MeetName = meetViewModel.Meet.MeetName;
            meet.Date = meetViewModel.Meet.Date;
            meet.PoolLength = meetViewModel.Meet.PoolLength;

            if (ModelState.IsValid)
            {
                _meetRepository.UpdateMeet(meet);
                _meetRepository.Save();
                return RedirectToAction("Index");
            }
            return View(meetViewModel);
        }

        // GET: Meet/Delete/5
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
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
        [CustomAuthorize(Roles = "Admin")]
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
