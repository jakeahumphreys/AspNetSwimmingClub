namespace MVCWebAssignment1.Migrations.MigrationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "MeetId", "dbo.Meets");
            DropForeignKey("dbo.Rounds", "EventId", "dbo.Events");
            DropForeignKey("dbo.Meets", "VenueId", "dbo.Venues");
            DropIndex("dbo.Events", new[] { "MeetId" });
            DropIndex("dbo.Meets", new[] { "VenueId" });
            DropIndex("dbo.Rounds", new[] { "EventId" });
            RenameColumn(table: "dbo.Events", name: "MeetId", newName: "Meet_Id");
            RenameColumn(table: "dbo.Rounds", name: "EventId", newName: "Event_Id");
            RenameColumn(table: "dbo.Meets", name: "VenueId", newName: "Venue_Id");
            RenameColumn(table: "dbo.Lanes", name: "SwimmerId", newName: "Swimmer_Id");
            RenameIndex(table: "dbo.Lanes", name: "IX_SwimmerId", newName: "IX_Swimmer_Id");
            AlterColumn("dbo.Events", "Meet_Id", c => c.Int());
            AlterColumn("dbo.Meets", "Venue_Id", c => c.Int());
            AlterColumn("dbo.Rounds", "Event_Id", c => c.Int());
            CreateIndex("dbo.Events", "Meet_Id");
            CreateIndex("dbo.Meets", "Venue_Id");
            CreateIndex("dbo.Rounds", "Event_Id");
            AddForeignKey("dbo.Events", "Meet_Id", "dbo.Meets", "Id");
            AddForeignKey("dbo.Rounds", "Event_Id", "dbo.Events", "Id");
            AddForeignKey("dbo.Meets", "Venue_Id", "dbo.Venues", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meets", "Venue_Id", "dbo.Venues");
            DropForeignKey("dbo.Rounds", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Events", "Meet_Id", "dbo.Meets");
            DropIndex("dbo.Rounds", new[] { "Event_Id" });
            DropIndex("dbo.Meets", new[] { "Venue_Id" });
            DropIndex("dbo.Events", new[] { "Meet_Id" });
            AlterColumn("dbo.Rounds", "Event_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Meets", "Venue_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Meet_Id", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Lanes", name: "IX_Swimmer_Id", newName: "IX_SwimmerId");
            RenameColumn(table: "dbo.Lanes", name: "Swimmer_Id", newName: "SwimmerId");
            RenameColumn(table: "dbo.Meets", name: "Venue_Id", newName: "VenueId");
            RenameColumn(table: "dbo.Rounds", name: "Event_Id", newName: "EventId");
            RenameColumn(table: "dbo.Events", name: "Meet_Id", newName: "MeetId");
            CreateIndex("dbo.Rounds", "EventId");
            CreateIndex("dbo.Meets", "VenueId");
            CreateIndex("dbo.Events", "MeetId");
            AddForeignKey("dbo.Meets", "VenueId", "dbo.Venues", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rounds", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Events", "MeetId", "dbo.Meets", "Id", cascadeDelete: true);
        }
    }
}
