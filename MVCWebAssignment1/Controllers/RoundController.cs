using MVCWebAssignment1.Customisations;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;
using MVCWebAssignment1.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCWebAssignment1.Controllers
{
    public class RoundController : Controller
    {
        private RoundService _roundService;

        public RoundController()
        {
            _roundService = new RoundService();
        }

        public RoundController(IRoundRepository roundRepository, ILaneRepository laneRepository)
        {
            this._roundService = new RoundService(roundRepository, laneRepository);
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(_roundService.GetIndex());
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(int EventId)
        {
            ServiceResponse response = _roundService.CreateAction(EventId);

            if (response.Result == true)
            {
                return RedirectToAction("Details", "Event", new { @id = EventId });
            }
            else
            {
                return View();
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            try
            {
                return View(_roundService.GetDetails(id));
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
        public ActionResult Delete(int id)
        {
            try
            {
                return View(_roundService.DeleteView(id));
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
            ServiceResponse response = _roundService.DeleteAction(id);

            if (response.Result == true)
            {
                return RedirectToAction("Details", "Event", new { @id = response.ReturnInt });
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
                _roundService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}