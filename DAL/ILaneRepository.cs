using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public interface ILaneRepository : IDisposable
    {
        IList<Lane> GetLanes();
        Lane GetLaneById(int id);
        void InsertLane(Lane lane);
        void DeleteLane(Lane lane);
        void UpdateLane(Lane lane);
        void Save();
    }
}