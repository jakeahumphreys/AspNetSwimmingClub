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
    public class EventController : Controller
    {
        private EventRepository _eventRepository;
        private MeetRepository _meetRepository;
        private RoundRepository _roundRepository;

        public EventController()
        {
            _eventRepository = new EventRepository(new EventContext());
            _meetRepository = new MeetRepository(new MeetContext());
            _roundRepository = new RoundRepository(new RoundContext());
        }
        // GET: Event
        public ActionResult Index()
        {
            return View(_eventRepository.GetEvents());
        }

        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventViewModel eventViewModel = new EventViewModel();

            Event @event = _eventRepository.GetEventById(id);

            if (@event == null)
            {
                return HttpNotFound();
            }

            //Get Rounds for this event
            List<Round> RelatedRounds = new List<Round>();
            int EventId = @event.Id;
            foreach (var round in _roundRepository.GetRounds())
            {
                if (round.EventId != 0)
                {
                    if (round.EventId == EventId)
                    {
                        RelatedRounds.Add(round);
                    }
                }
            }

            //Add components to ViewModel
            eventViewModel.Event = @event;
            eventViewModel.Rounds = RelatedRounds;

            //Return View
            return View(eventViewModel);
        }

        // GET: Event/Create
        public ActionResult Create(int MeetId)
        {
            EventViewModel eventViewModel = new EventViewModel();
            eventViewModel.MeetId = MeetId;
            ViewBag.MeetId = MeetId;
            return View(eventViewModel);
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                if(eventViewModel.MeetId > 0)
                {
                    eventViewModel.Event.MeetId = eventViewModel.MeetId;
                }
                _eventRepository.InsertEvent(eventViewModel.Event);
                _eventRepository.Save();
                return RedirectToAction("Details", "Meet", new {@id = eventViewModel.MeetId});
            }

            return View(eventViewModel);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = _eventRepository.GetEventById(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event @event)
        {
            Event eventToUpdate = _eventRepository.GetEventById(@event.Id);

            eventToUpdate.AgeRange = @event.AgeRange;
            eventToUpdate.Distance = @event.Distance;
            eventToUpdate.Gender = @event.Gender;
            eventToUpdate.SwimmingStroke = @event.SwimmingStroke;

            if (ModelState.IsValid)
            {
                _eventRepository.UpdateEvent(eventToUpdate);
                _eventRepository.Save();
                return RedirectToAction("Details", "Event", new { @id = eventToUpdate.Id});
            }
            return View(@event);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = _eventRepository.GetEventById(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
 
            Event @event = _eventRepository.GetEventById(id);
            var meetId = @event.MeetId;
            _eventRepository.DeleteEvent(@event);
            _eventRepository.Save();
            return RedirectToAction("Details", "Meet", new { @id = meetId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _eventRepository.Dispose();
                _meetRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
