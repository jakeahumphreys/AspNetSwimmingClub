using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCWebAssignment1.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCWebAssignment1.DAL
{
    public class FamilyGroupContext : IdentityDbContext<ApplicationUser>
    {
        public FamilyGroupContext() : base("DefaultConnection")
        {
        }
        public DbSet<FamilyGroup> FamilyGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}