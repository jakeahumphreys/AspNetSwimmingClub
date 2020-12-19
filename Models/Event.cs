using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string AgeRange { get; set; }
        public string Gender { get; set; }
        public string Distance { get; set; }
        public string SwimmingStroke { get; set; }
        public ICollection<Round> Rounds { get; set; }
        public virtual Meet Meet { get; set; }
    }
}