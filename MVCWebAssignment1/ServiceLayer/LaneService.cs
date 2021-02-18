using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.ServiceLayer
{
    public class LaneService
    {
        private readonly ILaneRepository _laneRepository;
        private readonly ApplicationDbContext _applicationDbContext;

        public LaneService()
        {
            _laneRepository = new LaneRepository(new LaneContext());
            _applicationDbContext = new ApplicationDbContext();
        }

        public LaneService(ILaneRepository laneRepository, ApplicationDbContext _applicationDbContext)
        {
            this._laneRepository = laneRepository;
            this._applicationDbContext = _applicationDbContext;
        }

        public List<Lane> GetIndex()
        {
            return _laneRepository.GetLanes().ToList();
        }

        public Lane EditView(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected Integer");
            }
            Lane lane = _laneRepository.GetLaneById(id);

            if (lane == null)
            {
                throw new HttpException("Lane not found.");
            }
            return lane;
        }

        public ServiceResponse EditAction(Lane lane)
        {
            Lane laneToUpdate = _laneRepository.GetLaneById(lane.Id);

            if(laneToUpdate != null)
            {
                laneToUpdate.FinishTime = lane.FinishTime;
                laneToUpdate.LaneComment = lane.LaneComment;

                _laneRepository.UpdateLane(laneToUpdate);
                _laneRepository.Save();

                return new ServiceResponse { Result = true };
            }
            else
            {
                return new ServiceResponse { Result = false, ServiceObject = lane };
            }
        }

        public LaneViewModel CreateView(int id)
        {
            LaneViewModel laneViewModel = new LaneViewModel();
            laneViewModel.RoundId = id;

            if (_applicationDbContext != null)
            {
                laneViewModel.AvailableSwimmers = _applicationDbContext.Users.Where(x => x.IsAllowedToSwim == true).ToList();
            }

            return laneViewModel;
        }

        public ServiceResponse CreateAction(LaneViewModel laneViewModel)
        {
            var lane = new Lane
            {
                RoundId = laneViewModel.RoundId,
                LaneNumber = _laneRepository.GetLanes().Where(x => x.RoundId == laneViewModel.RoundId).ToList().Count + 1,
                SwimmerId = laneViewModel.UserId
        };

          
            _laneRepository.InsertLane(lane);
            _laneRepository.Save();

            return new ServiceResponse { Result = true };
        }

        public Lane DeleteView(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected Integer");
            }
            Lane lane = _laneRepository.GetLaneById(id);

            if (lane == null)
            {
                throw new HttpException("Lane not found.");
            }
            return lane;
        }

        public ServiceResponse DeleteAction(int id)
        {
            Lane lane = _laneRepository.GetLaneById(id);

            if(lane != null)
            {
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

                return new ServiceResponse { Result = true, ReturnInt = RoundId};
            }
            else
            {
                return new ServiceResponse { Result = false};
            }
        }

        public void Dispose()
        {
            _laneRepository.Dispose();
        }
    }
}