using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class Round
    {
        public int Id { get; set; }
        public ICollection<Lane> Lanes { get; set; }
        //EF6 Reference
        public Event Event { get; set; }
    }
}