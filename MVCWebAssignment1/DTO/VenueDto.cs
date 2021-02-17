using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.DTO
{
    [DataContract]
    public class VenueDto
    {
        [DataMember(Name = "Venue_ID")]
        public int Id { get; set; }
        [DataMember(Name = "Venue_Name")]
        public string VenueName { get; set; }
        [DataMember(Name="Venue_Address")]
        public string Address { get; set; }
        [DataMember(Name="Venue_Meets")]
        public ICollection<MeetDto> Meets { get; set; }
    }
}