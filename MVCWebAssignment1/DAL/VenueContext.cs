using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCWebAssignment1.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCWebAssignment1.DAL
{
    public class VenueContext : IdentityDbContext<ApplicationUser>
    {
        public VenueContext() : base("DefaultConnection") {
        }
        public DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}