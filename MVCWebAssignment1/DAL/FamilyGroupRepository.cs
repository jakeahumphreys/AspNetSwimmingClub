using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public class FamilyGroupRepository : IFamilyGroupRepository
    {
        private readonly FamilyGroupContext _context;

        public FamilyGroupRepository(FamilyGroupContext context)
        {
            _context = context;
        }
        public IList<FamilyGroup> GetFamilyGroups()
        {
            return _context.FamilyGroups.Include(x => x.ApplicationUser).ToList();
        }

        public FamilyGroup GetFamilyGroupById(int id)
        {
            return _context.FamilyGroups.Where(x => x.FamilyGroupId == id).Include(x => x.ApplicationUser).SingleOrDefault();
        }

        public void InsertFamilyGroup(FamilyGroup familyGroup)
        {
            _context.FamilyGroups.Add(familyGroup);
        }

        public void DeleteFamilyGroup(FamilyGroup familyGroup)
        {
            _context.FamilyGroups.Remove(familyGroup);
        }

        public void UpdateFamilyGroup(FamilyGroup familyGroup)
        {
            _context.Entry(familyGroup).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}