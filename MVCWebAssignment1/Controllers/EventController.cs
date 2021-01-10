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
using MVCWebAssignment1.ServiceLayer;

namespace MVCWebAssignment1.Controllers
{
    public class EventController : Controller
    {
        private EventService _eventService;

        public EventController()
        {
            _eventService = new EventService();
        }

        public EventController(IEventRepository eventRepository, IMeetRepository meetRepository, IRoundRepository roundRepository)
        {
            this._eventService = new EventService(eventRepository, meetRepository, roundRepository);
        }
        // GET: Event
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(_eventService.GetIndex());
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            try
            {
                return View(_eventService.GetDetails(id));
            }
            catch(ArgumentException ex)
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.Service, message = ex.Message });
            }
            catch (HttpException ex)
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.Service, message = ex.Message });
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(int MeetId)
        {
            return View(_eventService.CreateView(MeetId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(EventViewModel eventViewModel)
        {
            ServiceResponse response = _eventService.CreateAction(eventViewModel);

            if(response.Result == true)
            {
                return RedirectToAction("Details", "Meet", new { @id = eventViewModel.MeetId });
            }
            else
            {
                return View(eventViewModel);
            }
        }

        // GET: Event/Edit/5
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            try
            {
                return View(_eventService.EditView(id));
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.Service, message = ex.Message });
            }
            catch (HttpException ex)
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.Service, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(Event @event)
        {
            ServiceResponse response = _eventService.EditAction(@event);

            if(response.Result == true)
            {
                return RedirectToAction("Details", "Event", new { @id = @event.Id });
            }
            else
            {
                return View(@event);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                return View(_eventService.DeleteView(id));
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.Service, message = ex.Message });
            }
            catch (HttpException ex)
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.Service, message = ex.Message });
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceResponse response = _eventService.DeleteAction(id);

            if (response.Result == true)
            {
                return RedirectToAction("Index", "Meet", null);
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
                _eventService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
