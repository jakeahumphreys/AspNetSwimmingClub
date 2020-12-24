namespace MVCWebAssignment1.Migrations.MigrationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Meet_Id", "dbo.Meets");
            DropForeignKey("dbo.Rounds", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Meets", "Venue_Id", "dbo.Venues");
            DropIndex("dbo.Events", new[] { "Meet_Id" });
            DropIndex("dbo.Meets", new[] { "Venue_Id" });
            DropIndex("dbo.Rounds", new[] { "Event_Id" });
            RenameColumn(table: "dbo.Events", name: "Meet_Id", newName: "MeetId");
            RenameColumn(table: "dbo.Rounds", name: "Event_Id", newName: "EventId");
            RenameColumn(table: "dbo.Meets", name: "Venue_Id", newName: "VenueId");
            AlterColumn("dbo.Events", "MeetId", c => c.Int(nullable: false));
            AlterColumn("dbo.Meets", "VenueId", c => c.Int(nullable: false));
            AlterColumn("dbo.Rounds", "EventId", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "MeetId");
            CreateIndex("dbo.Meets", "VenueId");
            CreateIndex("dbo.Rounds", "EventId");
            AddForeignKey("dbo.Events", "MeetId", "dbo.Meets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rounds", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Meets", "VenueId", "dbo.Venues", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meets", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Rounds", "EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "MeetId", "dbo.Meets");
            DropIndex("dbo.Rounds", new[] { "EventId" });
            DropIndex("dbo.Meets", new[] { "VenueId" });
            DropIndex("dbo.Events", new[] { "MeetId" });
            AlterColumn("dbo.Rounds", "EventId", c => c.Int());
            AlterColumn("dbo.Meets", "VenueId", c => c.Int());
            AlterColumn("dbo.Events", "MeetId", c => c.Int());
            RenameColumn(table: "dbo.Meets", name: "VenueId", newName: "Venue_Id");
            RenameColumn(table: "dbo.Rounds", name: "EventId", newName: "Event_Id");
            RenameColumn(table: "dbo.Events", name: "MeetId", newName: "Meet_Id");
            CreateIndex("dbo.Rounds", "Event_Id");
            CreateIndex("dbo.Meets", "Venue_Id");
            CreateIndex("dbo.Events", "Meet_Id");
            AddForeignKey("dbo.Meets", "Venue_Id", "dbo.Venues", "Id");
            AddForeignKey("dbo.Rounds", "Event_Id", "dbo.Events", "Id");
            AddForeignKey("dbo.Events", "Meet_Id", "dbo.Meets", "Id");
        }
    }
}
