namespace MVCWebAssignment1.Migrations.MigrationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSwimmerId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Lanes", name: "Swimmer_Id", newName: "SwimmerId");
            RenameIndex(table: "dbo.Lanes", name: "IX_Swimmer_Id", newName: "IX_SwimmerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Lanes", name: "IX_SwimmerId", newName: "IX_Swimmer_Id");
            RenameColumn(table: "dbo.Lanes", name: "SwimmerId", newName: "Swimmer_Id");
        }
    }
}
