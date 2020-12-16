namespace MVCWebAssignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDuplicatePhoneNumber : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "TelephoneNumber");
        }
        
        public override void Down()
        {
        }
    }
}
