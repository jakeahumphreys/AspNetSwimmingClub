using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class EditUserViewModel
    {
        [Display(Name ="Family Group ID")]
        public string FamilyGroupId { get; set; }
        public List<FamilyGroup> FamilyGroups { get; set; }
        public ApplicationUser User { get; set; }
    }
}