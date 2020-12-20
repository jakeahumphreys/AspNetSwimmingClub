namespace MVCWebAssignment1.Migrations.MigrationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoundNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rounds", "RoundNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rounds", "RoundNumber");
        }
    }
}
