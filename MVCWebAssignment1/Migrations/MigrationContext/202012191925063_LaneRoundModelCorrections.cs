namespace MVCWebAssignment1.Migrations.MigrationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LaneRoundModelCorrections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lanes", "round_Id", "dbo.Rounds");
            DropIndex("dbo.Lanes", new[] { "round_Id" });
            RenameColumn(table: "dbo.Lanes", name: "round_Id", newName: "RoundId");
            AlterColumn("dbo.Lanes", "RoundId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lanes", "RoundId");
            AddForeignKey("dbo.Lanes", "RoundId", "dbo.Rounds", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lanes", "RoundId", "dbo.Rounds");
            DropIndex("dbo.Lanes", new[] { "RoundId" });
            AlterColumn("dbo.Lanes", "RoundId", c => c.Int());
            RenameColumn(table: "dbo.Lanes", name: "RoundId", newName: "round_Id");
            CreateIndex("dbo.Lanes", "round_Id");
            AddForeignKey("dbo.Lanes", "round_Id", "dbo.Rounds", "Id");
        }
    }
}
