using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public class RoundRepository : IRoundRepository
    {
        private readonly RoundContext _context;

        public RoundRepository(RoundContext context)
        {
            _context = context;
        }
        public IList<Round> GetRounds()
        {
            return _context.Rounds.Include(x => x.Lanes.Select(y => y.Swimmer)).ToList();
        }

        public Round GetRoundById(int id)
        {
            return _context.Rounds.Where(x => x.Id == id).Include(x => x.Lanes).SingleOrDefault();
        }

        public void InsertRound(Round round)
        {
            _context.Rounds.Add(round);
        }

        public void DeleteRound(Round round)
        {
            _context.Rounds.Remove(round);
        }

        public void UpdateRound(Round round)
        {
            _context.Entry(round).State = EntityState.Modified;
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