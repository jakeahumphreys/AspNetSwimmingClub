using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class Venue
    {
        public int Id { get; set; }

        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }

        public ICollection<Meet> Meets { get; set; }
    }
}