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

        public EventController()
        {
            _eventRepository = new EventRepository(new EventContext());
            _meetRepository = new MeetRepository(new MeetContext());
        }
        // GET: Event
        public ActionResult Index()
        {
            return View(_eventRepository.GetEvents());
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Event @event = _eventRepository.GetEventById(id);
            ViewBag.MeetId = @event.Meet.Id;

            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
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
                    Meet meet = _meetRepository.GetMeetById(eventViewModel.MeetId);
                    if(meet != null)
                    {
                        eventViewModel.Event.Meet = meet;
                    }
                }
                _eventRepository.InsertEvent(eventViewModel.Event);
                _eventRepository.Save();
                return RedirectToAction("Index");
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
        public ActionResult Edit([Bind(Include = "Id,AgeRange,Gender,Distance,SwimmingStroke")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _eventRepository.UpdateEvent(@event);
                _eventRepository.Save();
                return RedirectToAction("Index");
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
            _eventRepository.DeleteEvent(@event);
            _eventRepository.Save();
            return RedirectToAction("Index");
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
