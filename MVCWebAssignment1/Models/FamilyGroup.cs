using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCWebAssignment1.Models
{
    public class FamilyGroup
    {
        public int FamilyGroupId { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}