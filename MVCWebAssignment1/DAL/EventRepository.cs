using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public class EventRepository : IEventRepository
    {
        private readonly EventContext _context;

        public EventRepository(EventContext context)
        {
            _context = context;
        }
        public IList<Event> GetEvents()
        {
            return _context.Events.Include(x => x.Meet).Include(x => x.Meet.Venue).Include(x => x.Rounds.Select(y => y.Lanes.Select(z => z.Swimmer))).ToList();
        }

        public Event GetEventById(int id)
        {
            return _context.Events.Where(x => x.Id == id).Include(x => x.Meet).Include(x => x.Meet.Venue).Include(x => x.Rounds.Select(y => y.Lanes.Select(z => z.Swimmer))).SingleOrDefault();
        }

        public void InsertEvent(Event @event)
        {
            _context.Events.Add(@event);
        }

        public void DeleteEvent(Event @event)
        {
            _context.Events.Remove(@event);
        }

        public void UpdateEvent(Event @event)
        {
            _context.Entry(@event).State = EntityState.Modified;
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