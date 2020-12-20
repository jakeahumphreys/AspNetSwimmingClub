using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class EventViewModel
    {
        public int MeetId { get; set; }
        public Event Event { get; set; }
        public IList<Round> Rounds { get; set; }
    }
}