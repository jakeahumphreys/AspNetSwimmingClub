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
        public int VenueId { get; set; }
        [Display(Name = "Venue")]
        public Venue Venue { get; set; }
        public ICollection<Event> Events { get; set; }
        
    }
}