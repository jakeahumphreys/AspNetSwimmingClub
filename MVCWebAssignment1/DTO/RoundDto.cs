using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.DTO
{
    [DataContract]
    public class RoundDto
    {
        [DataMember(Name = "Round_ID")]
        public int Id { get; set; }
        [DataMember(Name = "Round_Number")]
        public int RoundNumber { get; set; }
        [DataMember(Name = "Round_Lanes")]
        public ICollection<LaneDto> Lanes { get; set; }
    }
}