using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public interface IFamilyGroupRepository : IDisposable
    {
        IList<FamilyGroup> GetFamilyGroups();
        FamilyGroup GetFamilyGroupById(int id);
        void InsertFamilyGroup(FamilyGroup familyGroup);
        void DeleteFamilyGroup(FamilyGroup familyGroup);
        void UpdateFamilyGroup(FamilyGroup familyGroup);
        void Save();
    }
}