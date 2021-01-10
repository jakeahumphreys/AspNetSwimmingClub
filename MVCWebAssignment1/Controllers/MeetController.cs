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
    public class MeetController : Controller
    {

        private MeetService _meetService;

        public MeetController()
        {
            _meetService = new MeetService();
        }

        public MeetController(IMeetRepository meetRepository, IEventRepository eventRepository, IVenueRepository venueRepository)
        {
            this._meetService = new MeetService(meetRepository, eventRepository, venueRepository);
        }

        public ActionResult Index(string searchParamDateLower, string searchParamDateUpper)
        {
            return View(_meetService.GetIndex(searchParamDateLower, searchParamDateUpper));
        }

        public ActionResult Details(int id)
        {
            try
            {
                return View(_meetService.GetDetails(id));
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
            try
            {
                return View(_meetService.CreateView());
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
        public ActionResult Create(MeetViewModel meetViewModel)
        {
            ServiceResponse response = _meetService.CreateAction(meetViewModel);

            if (response.Result == true)
            {
                return RedirectToAction("Details", "Meet", new { @id = meetViewModel.Meet.Id });
            }
            else
            {
                return View(response.ServiceObject as MeetViewModel);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            try
            {
                return View(_meetService.EditView(id));
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
        public ActionResult Edit(MeetViewModel meetViewModel)
        {
            ServiceResponse response = _meetService.EditAction(meetViewModel);

            if (response.Result == true)
            {
                return RedirectToAction("Details", "Meet", new { @id = meetViewModel.Meet.Id });
            }
            else
            {
                return View(response.ServiceObject as MeetViewModel);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                return View(_meetService.DeleteView(id));
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
            ServiceResponse response = _meetService.DeleteAction(id);

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
                _meetService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
