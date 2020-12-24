using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class EditUserViewModel
    {
        public string FamilyGroupId { get; set; }
        public List<FamilyGroup> FamilyGroups { get; set; }
        public ApplicationUser User { get; set; }
    }
}