using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Display(Name = "Age Range")]
        public string AgeRange { get; set; }
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Distance")]
        public string Distance { get; set; }
        [Display(Name = "Swimming Stroke")]
        public string SwimmingStroke { get; set; }
        public ICollection<Round> Rounds { get; set; }
        public int MeetId { get; set; }
        public Meet Meet { get; set; }
    }
}