using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public class MeetRepository : IMeetRepository
    {
        private readonly MeetContext _context;
        
        public MeetRepository(MeetContext context)
        {
            _context = context;
        }
        public IList<Meet> GetMeets()
        {
            return _context.Meets.Include(x => x.Venue).Include(x => x.Events).Include(x => x.Events.Select(y => y.Rounds.Select(z=> z.Lanes.Select(q => q.Swimmer)))).ToList();
        }

        public Meet GetMeetById(int id)
        {
            return _context.Meets.Where(x => x.Id == id).Include(x => x.Venue).Include(x => x.Events).SingleOrDefault();
        }

        public void InsertMeet(Meet meet)
        {
            _context.Meets.Add(meet);
        }

        public void DeleteMeet(Meet meet)
        {
            _context.Meets.Remove(meet);
        }

        public void UpdateMeet(Meet meet)
        {
            _context.Entry(meet).State = EntityState.Modified;
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
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