using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class Round
    {
        public int Id { get; set; }
        [Display(Name = "Round #")]
        public int RoundNumber { get; set; }
        public ICollection<Lane> Lanes { get; set; }
        //EF6 Reference
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}