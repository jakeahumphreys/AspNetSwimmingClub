using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.ServiceLayer
{
    public class VenueService
    {
        private readonly IVenueRepository _venueRepository;

        public VenueService()
        {
            _venueRepository = new VenueRepository(new VenueContext());
        }

        public VenueService(IVenueRepository venueRepository)
        {
            this._venueRepository = venueRepository;
        }

        public List<Venue> GetIndex()
        {
            return _venueRepository.GetVenues().ToList();
        }

        public Venue GetDetails(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected integer");
            }
            Venue venue = _venueRepository.GetVenueById(id);

            if (venue == null)
            {
                throw new HttpException("Venue not found");
            }
            return venue;
        }

        public ServiceResponse CreateAction(Venue venue)
        {
            if(venue != null)
            {
                _venueRepository.InsertVenue(venue);
                _venueRepository.Save();

                return new ServiceResponse { Result = true };
            }
            else
            {
                return new ServiceResponse { Result = false };
            }
        }

        public Venue EditView(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected integer");
            }
            Venue venue = _venueRepository.GetVenueById(id);

            if (venue == null)
            {
                throw new HttpException("Venue not found");
            }
            return venue;
        }

        public ServiceResponse EditAction(Venue venue)
        {
            if(venue != null)
            {
                _venueRepository.UpdateVenue(venue);
                _venueRepository.Save();

                return new ServiceResponse { Result = true };
            }
            else
            {
                return new ServiceResponse { Result = false };
            }
        }

        public Venue DeleteView(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected integer");
            }
            Venue venue = _venueRepository.GetVenueById(id);

            if (venue == null)
            {
                throw new HttpException("Venue not found");
            }
            return venue;
        }

        public ServiceResponse DeleteAction(int id)
        {
            Venue venue = _venueRepository.GetVenueById(id);

            if(venue != null)
            {
                _venueRepository.DeleteVenue(venue);
                _venueRepository.Save();

                return new ServiceResponse { Result = true };
            }
            else
            {
                return new ServiceResponse { Result = false };
            }
        }

        public void Dispose()
        {
            _venueRepository.Dispose();
        }
    }
}