namespace MVCWebAssignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserBooleanFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsAllowedToSwim", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsAllowedToSwim");
            DropColumn("dbo.AspNetUsers", "IsActive");
        }
    }
}
