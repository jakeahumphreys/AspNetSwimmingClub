namespace MVCWebAssignment1.Migrations.MigrationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLaneNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lanes", "LaneNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lanes", "LaneNumber");
        }
    }
}
