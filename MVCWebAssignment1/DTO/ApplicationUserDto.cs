using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MVCWebAssignment1.DTO
{
    [DataContract]
    public class ApplicationUserDto
    {
        [DataMember(Name= "User_ID")]
        public string Id { get; set; }
        [DataMember(Name = "User_Name")]
        public string Name { get; set; }
        [DataMember(Name = "User_Gender")]
        public string Gender { get; set; }
        [DataMember(Name = "User_Address")]
        public string Address { get; set; }
        [DataMember(Name = "User_Date_Of_Birth")]
        public string DateOfBirth { get; set; }
        [DataMember(Name = "User_Family_ID")]
        public int FamilyGroupId { get; set; }
        [DataMember(Name = "User_Email")]
        public string Email { get; set; }
        [DataMember(Name = "User_Phone_Number")]
        public string PhoneNumber { get; set; }
        [DataMember(Name = "User_Permission_To_Swim")]
        public bool IsAllowedToSwim { get; set; }
        [DataMember(Name = "User_Lockout_End_Date")]
        public DateTime LockoutEndDateUtc { get; set; }
    }
}