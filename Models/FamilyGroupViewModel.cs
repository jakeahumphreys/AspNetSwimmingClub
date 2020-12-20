using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Models
{
    public class FamilyGroupViewModel
    {
        public FamilyGroup FamilyGroup { get; set; }
        public IList<ApplicationUser> FamilyGroupMembers { get; set; }
    }
}