namespace MVCWebAssignment1.Migrations.MigrationContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ConfigurationMigrationContext : DbMigrationsConfiguration<MVCWebAssignment1.DAL.MigrationContext>
    {
        public ConfigurationMigrationContext()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\MigrationContext";
        }

        protected override void Seed(MVCWebAssignment1.DAL.MigrationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
