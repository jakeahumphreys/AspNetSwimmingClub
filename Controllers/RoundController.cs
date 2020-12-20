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
    public class RoundController : Controller
    {
        private RoundRepository _roundRepository;
        private LaneRepository _laneRepository;

        public RoundController()
        {
            _roundRepository = new RoundRepository(new RoundContext());
            _laneRepository = new LaneRepository(new LaneContext());
        }

        // GET: Round
        public ActionResult Index()
        {
            return View(_roundRepository.GetRounds());
        }

        public ActionResult Create(int EventId)
        {
            int roundNumber = _roundRepository.GetRounds().Where(x => x.EventId == EventId).ToList().Count + 1;

            Round round = new Round();
            round.EventId = EventId;
            round.RoundNumber = roundNumber;
            if(ModelState.IsValid)
            {
                _roundRepository.InsertRound(round);
                _roundRepository.Save();
                return RedirectToAction("Details", "Event", new { @id = EventId });
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoundViewModel roundViewModel = new RoundViewModel();

            Round round = _roundRepository.GetRoundById(id);

            if (round == null)
            {
                return HttpNotFound();
            }

            //Get Lanes for this event
            List<Lane> RelatedLanes = new List<Lane>();
            int RoundId = round.Id;

            RelatedLanes = _laneRepository.GetLanes().Where(x => x.RoundId == RoundId).ToList();

            if(RelatedLanes != null)
            {
                roundViewModel.Lanes = RelatedLanes;

            }

            //Add components to ViewModel
            roundViewModel.Round = round;

            //Return View
            return View(roundViewModel);
        }

        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Round round = _roundRepository.GetRoundById(id);
            if (round == null)
            {
                return HttpNotFound();
            }
            return View(round);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Round round = _roundRepository.GetRoundById(id);
            var EventId = round.EventId;

            _roundRepository.DeleteRound(round);
            _roundRepository.Save();
            var counter = 0;
            foreach(var roundItem in _roundRepository.GetRounds().Where(x => x.EventId == EventId))
            {
                counter++;
                if(roundItem != null)
                {
                    roundItem.RoundNumber = counter;
                    _roundRepository.UpdateRound(roundItem);
                    _roundRepository.Save();
                }
            }
            return RedirectToAction("Details", "Event", new { @id = EventId });
        }
    }
}