using MVCWebAssignment1.Customisations;
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
        private ILaneRepository _laneRepository;
        private ApplicationDbContext _applicationDbContext;

        public LaneController()
        {
            _laneRepository = new LaneRepository(new LaneContext());
            _applicationDbContext = new ApplicationDbContext();
        }

        public LaneController(ILaneRepository laneRepository, ApplicationDbContext _applicationDbContext)
        {
            this._laneRepository = laneRepository;
            this._applicationDbContext = _applicationDbContext;
        }

        // GET: Lane
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(_laneRepository.GetLanes());
        }

        // GET: Event/Edit/5
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
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

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(Lane lane)
        {
            Lane laneToUpdate = _laneRepository.GetLaneById(lane.Id);

            laneToUpdate.FinishTime = lane.FinishTime;
            laneToUpdate.LaneComment = lane.LaneComment;

            if (ModelState.IsValid)
            {
                _laneRepository.UpdateLane(laneToUpdate);
                _laneRepository.Save();
                return RedirectToAction("Details", "Round", new { @id = laneToUpdate.RoundId });
            }
            return View(lane);
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(int RoundId)
        {
            LaneViewModel laneViewModel = new LaneViewModel();
            laneViewModel.RoundId = RoundId;
            ViewBag.RoundId = RoundId;

            if(_applicationDbContext != null)
            {
                laneViewModel.AvailableSwimmers = _applicationDbContext.Users.Where(x => x.IsAllowedToSwim == true).ToList();
            }

            return View(laneViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(LaneViewModel laneViewModel)
        {
            if (ModelState.IsValid)
            {
                laneViewModel.Lane.RoundId = laneViewModel.RoundId;
                laneViewModel.Lane.LaneNumber = _laneRepository.GetLanes().Where(x => x.RoundId == laneViewModel.RoundId).ToList().Count + 1;
                laneViewModel.Lane.SwimmerId = laneViewModel.UserId;
                _laneRepository.InsertLane(laneViewModel.Lane);
                _laneRepository.Save();
                return RedirectToAction("Details", "Round", new { @id = laneViewModel.RoundId});
            }

            return View(laneViewModel);
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
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
        [CustomAuthorize(Roles = "Admin")]
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
            return RedirectToAction("Details", "Round", new { @id = RoundId });
        }
    }
}