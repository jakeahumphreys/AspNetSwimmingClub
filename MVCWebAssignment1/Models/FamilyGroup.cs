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
        [Display(Name = "Family Group ID")]
        public int FamilyGroupId { get; set; }
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}