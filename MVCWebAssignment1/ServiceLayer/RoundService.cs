using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.ServiceLayer
{
    public class RoundService
    {
        private IRoundRepository _roundRepository;
        private ILaneRepository _laneRepository;

        public RoundService()
        {
            _roundRepository = new RoundRepository(new RoundContext());
            _laneRepository = new LaneRepository(new LaneContext());
        }

        public RoundService(IRoundRepository roundRepository, ILaneRepository laneRepository)
        {
            this._roundRepository = roundRepository;
            this._laneRepository = laneRepository;
        }

        public List<Round> GetIndex()
        {
            return _roundRepository.GetRounds().ToList();
        }

        public ServiceResponse CreateAction(int EventId)
        {
            int roundNumber = _roundRepository.GetRounds().Where(x => x.EventId == EventId).ToList().Count + 1;

            Round round = new Round();
            round.EventId = EventId;
            round.RoundNumber = roundNumber;

            if(round != null)
            {
                _roundRepository.InsertRound(round);
                _roundRepository.Save();

                return new ServiceResponse { Result = true };
            }
            else
            {
                return new ServiceResponse { Result = false };
            }
        }

        public RoundViewModel GetDetails(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected integer");
            }
            RoundViewModel roundViewModel = new RoundViewModel();

            Round round = _roundRepository.GetRoundById(id);

            if (round == null)
            {
                throw new HttpException("Round not found");
            }

            List<Lane> RelatedLanes = new List<Lane>();
            int RoundId = round.Id;

            RelatedLanes = _laneRepository.GetLanes().Where(x => x.RoundId == RoundId).ToList();

            if (RelatedLanes != null)
            {
                roundViewModel.Lanes = RelatedLanes;

            }

            roundViewModel.Round = round;

            return roundViewModel;
        }

        public Round DeleteView(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected integer");
            }

            Round round = _roundRepository.GetRoundById(id);

            if (round == null)
            {
                throw new HttpException("Round not found");
            }

            return round;
        }

        public ServiceResponse DeleteAction(int id)
        {
            Round round = _roundRepository.GetRoundById(id);

            if(round != null)
            {
                var EventId = round.EventId;

                _roundRepository.DeleteRound(round);
                _roundRepository.Save();
                var counter = 0;
                foreach (var roundItem in _roundRepository.GetRounds().Where(x => x.EventId == EventId))
                {
                    counter++;
                    if (roundItem != null)
                    {
                        roundItem.RoundNumber = counter;
                        _roundRepository.UpdateRound(roundItem);
                        _roundRepository.Save();
                    }
                }

                return new ServiceResponse { Result = true, ReturnInt = EventId};
            }
            else
            {
                return new ServiceResponse { Result = false };
            }
        }

        public void Dispose()
        {
            _roundRepository.Dispose();
        }
    }
}