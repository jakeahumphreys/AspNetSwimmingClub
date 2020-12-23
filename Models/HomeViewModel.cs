using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class HomeViewModel
    {
        public IList<Event> Events { get; set; }
        public IList<Event> PersonalEvents { get; set; }
        public IList<Lane> EventLanes { get; set; }
    }
}