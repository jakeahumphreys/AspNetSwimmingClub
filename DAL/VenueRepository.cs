using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public class VenueRepository : IVenueRepository
    {
        private readonly VenueContext _context;

        public VenueRepository(VenueContext context)
        {
            _context = context;
        }
        public IList<Venue> GetVenues()
        {
            return _context.Venues.ToList();
        }

        public Venue GetVenueById(int id)
        {
            return _context.Venues.Find(id);
        }

        public void InsertVenue(Venue venue)
        {
            _context.Venues.Add(venue);
        }

        public void DeleteVenue(Venue venue)
        {
            _context.Venues.Remove(venue);
        }

        public void UpdateVenue(Venue venue)
        {
            _context.Entry(venue).State = EntityState.Modified;
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