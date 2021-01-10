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
    public class VenueController : Controller
    {
        private VenueService _venueService;

        public VenueController()
        {
            _venueService = new VenueService();
        }

        public VenueController(IVenueRepository venueRepository)
        {
            this._venueService = new VenueService(venueRepository);
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(_venueService.GetIndex());
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            try
            {
                return View(_venueService.GetDetails(id));
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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(Venue venue)
        {

            ServiceResponse response = _venueService.CreateAction(venue);

            if (response.Result == true)
            {
                return RedirectToAction("Details", "Venue", new { @id = venue.Id});
            }
            else
            {
                return View(venue);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            try
            {
                return View(_venueService.EditView(id));
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
        public ActionResult Edit([Bind(Include = "Id,VenueName,Address")] Venue venue)
        {
            ServiceResponse response = _venueService.CreateAction(venue);

            if (response.Result == true)
            {
                return RedirectToAction("Details", "Venue", new { @id = venue.Id });
            }
            else
            {
                return View(venue);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                return View(_venueService.DeleteView(id));
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceResponse response = _venueService.DeleteAction(id);

            if (response.Result == true)
            {
                return RedirectToAction("Index", "Venue", null);
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
                _venueService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
