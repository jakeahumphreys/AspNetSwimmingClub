using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.ServiceLayer
{
    public class MeetService
    {
        private IMeetRepository _meetRepository;
        private IVenueRepository _venueRepository;
        private IEventRepository _eventRepository;

        public MeetService()
        {
            _meetRepository = new MeetRepository(new MeetContext());
            _venueRepository = new VenueRepository(new VenueContext());
            _eventRepository = new EventRepository(new EventContext());
        }

        public MeetService(IMeetRepository meetRepository, IEventRepository eventRepository, IVenueRepository venueRepository)
        {
            this._meetRepository = meetRepository;
            this._eventRepository = eventRepository;
            this._venueRepository = venueRepository;
        }

        public List<Meet> GetIndex(string searchParamDateLower, string searchParamDateUpper)
        {
            List<Meet> meets = _meetRepository.GetMeets().ToList();

            if (!String.IsNullOrEmpty(searchParamDateLower) && !String.IsNullOrEmpty(searchParamDateUpper))
            {
                List<Meet> updatedMeets = new List<Meet>();
                DateTime startDate = Convert.ToDateTime(searchParamDateLower);
                DateTime endDate = Convert.ToDateTime(searchParamDateUpper);

                foreach (var meet in meets)
                {
                    DateTime convertedDate = Convert.ToDateTime(meet.Date);

                    if (convertedDate >= startDate && convertedDate <= endDate)
                    {
                        updatedMeets.Add(meet);
                    }
                }
                meets = updatedMeets;               
            }
            return meets;
        }

        public MeetViewModel GetDetails(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected integer");
            }
            MeetViewModel meetViewModel = new MeetViewModel();

            Meet meet = _meetRepository.GetMeetById(id);

            if (meet == null)
            {
                throw new HttpException("Meet not found");
            }

            //Get Events for this meet
            List<Event> RelatedEvents = new List<Event>();
            foreach (var @event in _eventRepository.GetEvents())
            {
                if (@event.MeetId != 0)
                {
                    if (@event.MeetId == meet.Id)
                    {
                        RelatedEvents.Add(@event);
                    }

                }
            }

            //Add components to ViewModel
            meetViewModel.Meet = meet;
            meetViewModel.Events = RelatedEvents;

            return meetViewModel;
        }

        public Meet GetMeetDetails(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected integer");
            }

            Meet meet = _meetRepository.GetMeetById(id);

            if (meet == null)
            {
                throw new HttpException("Meet not found");
            }

            return meet;
        }

        public MeetViewModel CreateView()
        {
            MeetViewModel meetViewModel = new MeetViewModel();

            if (_venueRepository != null)
            {
                meetViewModel.Venues = _venueRepository.GetVenues();
            }

            return meetViewModel;

        }

        public ServiceResponse CreateAction(MeetViewModel meetViewModel)
        {
            int venueId;
            int.TryParse(meetViewModel.VenueId, out venueId); //Convert ID from DropDownList to Integer

            if(venueId != 0)
            {
                meetViewModel.Meet.VenueId = venueId;

                _meetRepository.InsertMeet(meetViewModel.Meet);
                _meetRepository.Save();

                return new ServiceResponse { Result = true };
            }
            else
            {
                return new ServiceResponse { Result = false, ServiceObject = meetViewModel };
            }
        }

        public MeetViewModel EditView(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected Integer");
            }

            MeetViewModel meetViewModel = new MeetViewModel();

            Meet meet = _meetRepository.GetMeetById(id);

            if (meet == null)
            {
                throw new HttpException("Meet not found.");
            }

            meetViewModel.Meet = meet;
            meetViewModel.Venues = _venueRepository.GetVenues();

            if (meetViewModel.Meet.VenueId != 0)
            {
                meetViewModel.VenueId = meetViewModel.Meet.VenueId.ToString();
            }

            return meetViewModel;
        }

        public ServiceResponse EditAction(MeetViewModel meetViewModel)
        {
            int venueId;
            int.TryParse(meetViewModel.VenueId, out venueId); //Convert ID from DropDownList to Integer

            meetViewModel.Meet.VenueId = venueId;

            Meet meet = _meetRepository.GetMeetById(meetViewModel.Meet.Id);

            if(meet != null)
            {
                meet.VenueId = meetViewModel.Meet.VenueId;
                meet.MeetName = meetViewModel.Meet.MeetName;
                meet.Date = meetViewModel.Meet.Date;
                meet.PoolLength = meetViewModel.Meet.PoolLength;

                _meetRepository.UpdateMeet(meet);
                _meetRepository.Save();

                return new ServiceResponse { Result = true };
            }
            else
            {
                return new ServiceResponse { Result = false, ServiceObject = meetViewModel };
            }
        }

        public Meet DeleteView(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected integer");
            }

            Meet meet = _meetRepository.GetMeetById(id);

            if (meet == null)
            {
                throw new HttpException("Meet not found");
            }

            return meet;
        }

        public ServiceResponse DeleteAction(int id)
        {
            Meet meet = _meetRepository.GetMeetById(id);

            if(meet != null)
            {
                foreach (var item in _eventRepository.GetEvents())
                {
                    if (item.Meet != null)
                    {
                        if (item.Meet.Id == id)
                        {
                            _eventRepository.DeleteEvent(item);
                        }
                    }

                }
                _meetRepository.DeleteMeet(meet);
                _meetRepository.Save();

                return new ServiceResponse { Result = true };
            }
            else
            {
                return new ServiceResponse { Result = false };
            }         
        }

        public void Dispose()
        {
            _meetRepository.Dispose();
            _eventRepository.Dispose();
            _venueRepository.Dispose();
        }
    }
}