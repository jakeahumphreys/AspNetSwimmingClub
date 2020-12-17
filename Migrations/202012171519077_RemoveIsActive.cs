namespace MVCWebAssignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsActive : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean(nullable: false));
        }
    }
}
