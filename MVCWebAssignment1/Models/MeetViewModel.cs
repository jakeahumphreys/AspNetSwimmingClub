using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class MeetViewModel
    {
        public Meet Meet { get; set; }
        public IList<Event> Events { get; set; }
        public string VenueId { get; set; }
        public IList<Venue> Venues { get; set; }
    }
}