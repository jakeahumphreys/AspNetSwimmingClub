using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    [NotMapped]
    public class LaneViewModel
    {
        public int RoundId { get; set; }
        public Lane Lane { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        public IList<ApplicationUser> AvailableSwimmers { get; set; }
    }
}