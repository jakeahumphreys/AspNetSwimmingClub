namespace MVCWebAssignment1.Migrations.MigrationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectFamilyGroupId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "FamilyGroup_FamilyGroupId", newName: "FamilyGroupId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_FamilyGroup_FamilyGroupId", newName: "IX_FamilyGroupId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_FamilyGroupId", newName: "IX_FamilyGroup_FamilyGroupId");
            RenameColumn(table: "dbo.AspNetUsers", name: "FamilyGroupId", newName: "FamilyGroup_FamilyGroupId");
        }
    }
}
