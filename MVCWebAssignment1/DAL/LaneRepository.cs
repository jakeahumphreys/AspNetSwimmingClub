using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public class LaneRepository : ILaneRepository
    {
        private readonly LaneContext _context;

        public LaneRepository(LaneContext context)
        {
            _context = context;
        }
        public IList<Lane> GetLanes()
        {
            return _context.Lanes.Include(x => x.Swimmer).ToList();
        }

        public Lane GetLaneById(int id)
        {
            return _context.Lanes.Where(x => x.Id == id).SingleOrDefault();
        }

        public void InsertLane(Lane lane)
        {
            _context.Lanes.Add(lane);
        }

        public void DeleteLane(Lane lane)
        {
            _context.Lanes.Remove(lane);
        }

        public void UpdateLane(Lane lane)
        {
            _context.Entry(lane).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}