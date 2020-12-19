using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class Lane
    {
        public int Id { get; set; }
        public virtual ApplicationUser Swimmer { get; set; }
        public string FinishTime { get; set; }
        public string LaneComment { get; set; }
        //Reference to round for EF6
        public virtual Round round { get; set; }
    }
}
