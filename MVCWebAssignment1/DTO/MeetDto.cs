using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc.Html;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.DTO
{
    [DataContract]
    public class MeetDto
    {
        [DataMember(Name = "Meet_ID")]
        public int Id { get; set; }
        [DataMember(Name = "Meet_Name")]
        public string MeetName { get; set; }
        [DataMember(Name = "Meet_Date")]
        public string Date { get; set; }
        [DataMember(Name = "Meet_Pool_Length")]
        public string PoolLength { get; set; }
        [DataMember(Name = "Meet_Venue_ID")]
        public int VenueId { get; set; }
        [DataMember(Name = "Meet_Events")]
        public ICollection<EventDto> Events { get; set; }
    }
}