using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    [NotMapped]
    public class RoundViewModel
    {
        public int EventId { get; set; }
        public Round Round { get; set; }
        public ApplicationUser Swimmer { get; set; }
        public IList<Lane> Lanes { get; set; }
        
    }
}