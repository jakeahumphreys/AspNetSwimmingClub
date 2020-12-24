using Microsoft.AspNet.Identity.EntityFramework;
using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public class RoundContext : IdentityDbContext<ApplicationUser>
    {
        public RoundContext() : base("DefaultConnection")
        {
        }
        public DbSet<Round> Rounds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}