namespace MVCWebAssignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "FamilyGroup_FamilyGroupId", "dbo.FamilyGroups");
            DropIndex("dbo.AspNetUsers", new[] { "FamilyGroup_FamilyGroupId" });
            DropColumn("dbo.AspNetUsers", "FamilyGroup_FamilyGroupId");
            DropTable("dbo.FamilyGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FamilyGroups",
                c => new
                    {
                        FamilyGroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.FamilyGroupId);
            
            AddColumn("dbo.AspNetUsers", "FamilyGroup_FamilyGroupId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "FamilyGroup_FamilyGroupId");
            AddForeignKey("dbo.AspNetUsers", "FamilyGroup_FamilyGroupId", "dbo.FamilyGroups", "FamilyGroupId");
        }
    }
}
