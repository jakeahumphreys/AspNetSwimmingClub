using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class Meet
    {
        public int Id { get; set; }
        [Display(Name ="Meet Name")]
        public string MeetName { get; set; }
        public string Date { get; set; }
        [Display(Name = "Pool Length (Metres)")]
        public string PoolLength { get; set; }
        [Display(Name = "Venue")]
        public virtual Venue MeetVenue { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        
    }
}