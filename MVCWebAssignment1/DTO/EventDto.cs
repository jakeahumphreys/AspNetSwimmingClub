using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.DTO
{
    [DataContract]
    public class EventDto
    {
        [DataMember(Name = "Event_ID")]
        public int Id { get; set; }
        [DataMember(Name = "Event_Age_Range")]
        public string AgeRange { get; set; }
        [DataMember(Name = "Event_Gender")]
        public string Gender { get; set; }
        [DataMember(Name = "Event_Distance")]
        public string Distance { get; set; }
        [DataMember(Name = "Event_Swimming_Stroke")]
        public string SwimmingStroke { get; set; }
        [DataMember(Name = "Event_Rounds")]
        public ICollection<RoundDto> Rounds { get; set; }
    }
}