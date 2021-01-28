using MVCWebAssignment1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCWebAssignment1.Models;

namespace MVCWebAssignment1.Common_Logic
{
    public class Library
    {

        private readonly FamilyGroupRepository _familyGroupRepository =
            new FamilyGroupRepository(new FamilyGroupContext());

        private readonly ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

        public int GetFamilyId(string userId)
        {
            var familyId = 0;

            if (userId != "")
            {
                var user = _applicationDbContext.Users.Find(userId);

                if (user?.FamilyGroupId != null)
                {
                    familyId = user.FamilyGroupId.Value;
                }
            }

            return familyId;
        }
    }
}