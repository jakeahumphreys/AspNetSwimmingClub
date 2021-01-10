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
    public class LaneController : Controller
    {

        private LaneService _laneService { get; set; }

        public LaneController()
        {
            _laneService = new LaneService();
        }

        public LaneController(ILaneRepository laneRepository, ApplicationDbContext _applicationDbContext)
        {
            _laneService = new LaneService(laneRepository, _applicationDbContext);
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(_laneService.GetIndex());
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            try
            {
                return View(_laneService.EditView(id));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(Lane lane)
        {
            ServiceResponse response = _laneService.EditAction(lane);

            if (response.Result == true)
            {
                return RedirectToAction("Details", "Round", new { @id = lane.RoundId});
            }
            else
            {
                return View(response.ServiceObject as Lane);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(int RoundId)
        {
            return View(_laneService.CreateView(RoundId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(LaneViewModel laneViewModel)
        {
            ServiceResponse response = _laneService.CreateAction(laneViewModel);

            if(response.Result == true)
            {
                return RedirectToAction("Details", "Round", new { @id = laneViewModel.RoundId });

            }
            else
            {
                return View(laneViewModel);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                return View(_laneService.DeleteView(id));
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
            ServiceResponse response = _laneService.DeleteAction(id);

            if (response.Result == true)
            {
                return RedirectToAction("Details", "Round", new { @id = response.ReturnInt});
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
                _laneService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}