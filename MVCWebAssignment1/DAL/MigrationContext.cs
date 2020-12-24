using Microsoft.AspNet.Identity.EntityFramework;
using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public class MigrationContext : IdentityDbContext<ApplicationUser>
    {
        public MigrationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Meet> Meets { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<FamilyGroup> FamilyGroups { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Lane> Lanes { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}