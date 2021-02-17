using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Web;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.DTO
{
    [DataContract]
    public class LaneDto
    {
        [DataMember(Name = "Lane_ID")]
        public int Id { get; set; }
        [DataMember(Name = "Lane_Number")]
        public int LaneNumber { get; set; }
        [DataMember(Name = "Lane_Swimmer_ID")]
        public string SwimmerId { get; set; }
        [DataMember(Name = "Lane_Swimmer")]
        public ApplicationUserDto Swimmer { get; set; }
        [DataMember(Name = "Lane_Finish_Time")]
        public string FinishTime { get; set; }
        [DataMember(Name = "Lane_Comment")]
        public string LaneComment { get; set; }
    }
}