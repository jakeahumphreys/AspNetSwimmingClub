using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.ServiceLayer
{
    public class EventService
    {
        private IEventRepository _eventRepository;
        private IMeetRepository _meetRepository;
        private IRoundRepository _roundRepository;

        public EventService()
        {
            _eventRepository = new EventRepository(new EventContext());
            _meetRepository = new MeetRepository(new MeetContext());
            _roundRepository = new RoundRepository(new RoundContext());
        }

        public EventService(IEventRepository eventRepository, IMeetRepository meetRepository, IRoundRepository roundRepository)
        {
            this._eventRepository = eventRepository;
            this._meetRepository = meetRepository;
            this._roundRepository = roundRepository;
        }

        public List<Event> GetIndex()
        {
            return _eventRepository.GetEvents().ToList();
        }

        public EventViewModel GetDetails(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected integer");
            }
            EventViewModel eventViewModel = new EventViewModel();

            Event @event = _eventRepository.GetEventById(id);

            if (@event == null)
            {
                throw new HttpException("Event not found");
            }

            //Get Rounds for this event
            List<Round> RelatedRounds = new List<Round>();
            int EventId = @event.Id;
            foreach (var round in _roundRepository.GetRounds())
            {
                if (round.EventId != 0)
                {
                    if (round.EventId == EventId)
                    {
                        RelatedRounds.Add(round);
                    }
                }
            }

            //Add components to ViewModel
            eventViewModel.Event = @event;
            eventViewModel.Rounds = RelatedRounds;

            //Return View
            return eventViewModel;
        }

        public EventViewModel CreateView(int id)
        {
            EventViewModel eventViewModel = new EventViewModel();
            eventViewModel.MeetId = id;
            return eventViewModel;
        }

        public ServiceResponse CreateAction(EventViewModel eventViewModel)
        {
            if (eventViewModel.MeetId > 0)
            {
                eventViewModel.Event.MeetId = eventViewModel.MeetId;
            }
            _eventRepository.InsertEvent(eventViewModel.Event);
            _eventRepository.Save();
            return new ServiceResponse { Result = true };
        }

        public Event EditView(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected integer");
            }
            Event @event = _eventRepository.GetEventById(id);
            if (@event == null)
            {
                throw new HttpException("Event not found");
            }
            return @event;
        }

        public ServiceResponse EditAction(Event @event)
        {
            Event eventToUpdate = _eventRepository.GetEventById(@event.Id);

            if(eventToUpdate != null)
            {
                eventToUpdate.AgeRange = @event.AgeRange;
                eventToUpdate.Distance = @event.Distance;
                eventToUpdate.Gender = @event.Gender;
                eventToUpdate.SwimmingStroke = @event.SwimmingStroke;

                _eventRepository.UpdateEvent(eventToUpdate);
                _eventRepository.Save();

                return new ServiceResponse { Result = true };
            }
            else
            {
                return new ServiceResponse { Result = false, ServiceObject = @event };
            }
        }

        public Event DeleteView(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected integer");
            }
            Event @event = _eventRepository.GetEventById(id);
            if (@event == null)
            {
                throw new HttpException("Event not found");
            }
            return @event;
        }

        public ServiceResponse DeleteAction(int id)
        {
            Event @event = _eventRepository.GetEventById(id);

            if(@event != null)
            {
                var meetId = @event.MeetId;
                _eventRepository.DeleteEvent(@event);
                _eventRepository.Save();

                return new ServiceResponse { Result = true };
            }
            else
            {
                return new ServiceResponse { Result = false, ServiceObject = @event };
            }
         
        }
        
        public int GetMeetFromEvent(int id)
        {
            if(id != 0)
            {                
                Event @event = _eventRepository.GetEventById(id);
                Meet meet = _meetRepository.GetMeetById(@event.MeetId);

                return meet.Id;
            }
            else
            {
                return 0;
            }
        }

        public void Dispose()
        {
            _eventRepository.Dispose();
            _meetRepository.Dispose();
        }
    }
}