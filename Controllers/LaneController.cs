using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;
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
        private LaneRepository _laneRepository;
        private ApplicationDbContext _applicationDbContext;

        public LaneController()
        {
            _laneRepository = new LaneRepository(new LaneContext());
            _applicationDbContext = new ApplicationDbContext();
        }

        // GET: Lane
        public ActionResult Index()
        {
            return View(_laneRepository.GetLanes());
        }

        public ActionResult Create(int RoundId)
        {
            LaneViewModel laneViewModel = new LaneViewModel();
            laneViewModel.RoundId = RoundId;
            ViewBag.RoundId = RoundId;

            laneViewModel.AvailableSwimmers = _applicationDbContext.Users.Where(x => x.IsAllowedToSwim == true).ToList();

            return View(laneViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LaneViewModel laneViewModel)
        {
            if (ModelState.IsValid)
            {
                laneViewModel.Lane.RoundId = laneViewModel.RoundId;
                laneViewModel.Lane.LaneNumber = _laneRepository.GetLanes().Where(x => x.RoundId == laneViewModel.RoundId).ToList().Count + 1;
                laneViewModel.Lane.SwimmerId = laneViewModel.UserId;
                _laneRepository.InsertLane(laneViewModel.Lane);
                _laneRepository.Save();
                return RedirectToAction("Details", "Meet", new { @id = laneViewModel.RoundId});
            }

            return View(laneViewModel);
        }

        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lane lane = _laneRepository.GetLaneById(id);
            if (lane == null)
            {
                return HttpNotFound();
            }
            return View(lane);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lane lane = _laneRepository.GetLaneById(id);
            var RoundId = lane.RoundId;

            _laneRepository.DeleteLane(lane);
            _laneRepository.Save();
            var counter = 0;
            foreach (var laneItem in _laneRepository.GetLanes().Where(x => x.RoundId == RoundId))
            {
                counter++;
                if (laneItem != null)
                {
                    laneItem.LaneNumber = counter;
                    _laneRepository.UpdateLane(laneItem);
                    _laneRepository.Save();
                }
            }
            return RedirectToAction("Details", "Event", new { @id = RoundId });
        }

        //// GET: Event/Create
        //public ActionResult Create(int RoundId)
        //{
        //    LaneViewModel laneViewModel = new LaneViewModel();
        //    laneViewModel.RoundId = RoundId;
        //    ViewBag.RoundId = RoundId;
        //    return View(laneViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(LaneViewModel laneViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (laneViewModel.RoundId > 0)
        //        {
        //            laneViewModel.Lane.RoundId = laneViewModel.RoundId;
        //        }
        //        _laneRepository.InsertLane(laneViewModel.Lane);
        //        _laneRepository.Save();
        //        return RedirectToAction("Details", "Event", new { @id = laneViewModel.RoundId });
        //    }

        //    return View(roundViewModel);
        //}
    }
}