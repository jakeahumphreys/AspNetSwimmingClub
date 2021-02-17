using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.DTO
{
    [DataContract]
    public class FamilyGroupDto
    {
        [DataMember(Name = "Family_ID")]
        public int FamilyGroupId { get; set; }
        [DataMember(Name= "Family_Name")]
        public string GroupName { get; set; }
        [DataMember(Name = "Family_Members")]
        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}