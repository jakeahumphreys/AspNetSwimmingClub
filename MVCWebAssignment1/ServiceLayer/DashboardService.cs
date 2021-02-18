using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MVCWebAssignment1.DAL;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.ServiceLayer
{
    public class DashboardService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly EventService _eventService;

        public DashboardService()
        {
            _applicationDbContext = new ApplicationDbContext();
            _eventService = new EventService();
        }

        public DashboardService(ApplicationDbContext applicationDbContext, IEventRepository eventRepository, IMeetRepository meetRepository, IRoundRepository roundRepository)
        {
            _applicationDbContext = applicationDbContext;
            _eventService = new EventService(eventRepository, meetRepository, roundRepository);
        }

        public List<ApplicationUser> SearchUserByName(string name)
        {
            var users = _applicationDbContext.Users.ToList();
           
            users = users.Where(x => x.Name.IndexOf(name, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();

            return users;
        }

        public List<ApplicationUser> SearchUserByStroke(string stroke)
        {
            var eventsIncludingStroke = _eventService.GetIndex().Where(x => x.SwimmingStroke.IndexOf(stroke, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
            var usersPartakingInEvents = new List<ApplicationUser>();
            foreach (var @event in eventsIncludingStroke)
            {
                foreach (var round in @event.Rounds)
                {
                    foreach (var lane in round.Lanes)
                    {
                        var user = _applicationDbContext.Users.Find(lane.SwimmerId);
                        if(!usersPartakingInEvents.Contains(user))
                        {
                            usersPartakingInEvents.Add(user);

                        }
                    }
                }
            }

            return usersPartakingInEvents;
        }

        public List<ApplicationUser> SearchUserByAge(int age)
        {
            var filteredUsers = new List<ApplicationUser>();
            var today = DateTime.Today;
            foreach (var user in _applicationDbContext.Users)
            {
                if (user.DateOfBirth != "")
                {
                    var dateOfBirth = Convert.ToDateTime(user.DateOfBirth);
                    var calculatedAge = today.Year - dateOfBirth.Year;

                    if (dateOfBirth.Date > today.AddYears(-calculatedAge)) calculatedAge--;

                    if (age == calculatedAge)
                    {
                        filteredUsers.Add(user);
                    }
                }
            }

            return filteredUsers;
        }
    }
}