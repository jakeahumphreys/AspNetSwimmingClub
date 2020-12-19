using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class MeetViewModel
    {
        public Meet Meet { get; set; }
        public List<Event> Events { get; set; }
        public string VenueId { get; set; }
        public List<Venue> Venues { get; set; }
    }
}