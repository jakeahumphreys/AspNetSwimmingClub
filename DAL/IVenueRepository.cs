using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public interface IVenueRepository : IDisposable
    {
        IList<Venue> GetVenues();
        Venue GetVenueById(int id);
        void InsertVenue(Venue venue);
        void DeleteVenue(Venue venue);
        void UpdateVenue(Venue venue);
        void Save();
    }
}