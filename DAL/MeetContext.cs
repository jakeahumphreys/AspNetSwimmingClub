using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCWebAssignment1.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCWebAssignment1.DAL
{
    public class MeetContext : IdentityDbContext<ApplicationUser>
    {
        public MeetContext(): base("DefaultConnection"){}
        public DbSet<Meet> Meets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}