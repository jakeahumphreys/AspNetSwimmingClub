using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public interface IEventRepository : IDisposable
    {
        IList<Event> GetEvents();
        Event GetEventById(int id);
        void InsertEvent(Event @event);
        void DeleteEvent(Event @event);
        void UpdateEvent(Event @event);
        void Save();
    }
}