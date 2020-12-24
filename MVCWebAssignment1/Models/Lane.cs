using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class Lane
    {
        public int Id { get; set; }
        [Display(Name = "Lane #")]
        public int LaneNumber { get; set; }
        [Display(Name = "Swimmer ID")]
        public string SwimmerId { get; set; }
        public ApplicationUser Swimmer { get; set; }
        [Display(Name = "Finish Time")]
        public string FinishTime { get; set; }
        [Display(Name = "Comment")]
        public string LaneComment { get; set; }
        //Reference to round for EF6
        [Display(Name = "Round ID")]
        public int RoundId { get; set; }
        public Round Round { get; set; }
    }
}
