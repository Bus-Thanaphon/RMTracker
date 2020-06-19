namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db58 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Works", "StartDateDay", c => c.Int(nullable: false));
            AddColumn("dbo.User_Works", "StartDateMonth", c => c.Int(nullable: false));
            AddColumn("dbo.User_Works", "EndODate", c => c.DateTime(nullable: false));
            AddColumn("dbo.User_Works", "EndDateDay", c => c.Int(nullable: false));
            AddColumn("dbo.User_Works", "EndDateMonth", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User_Works", "EndDateMonth");
            DropColumn("dbo.User_Works", "EndDateDay");
            DropColumn("dbo.User_Works", "EndODate");
            DropColumn("dbo.User_Works", "StartDateMonth");
            DropColumn("dbo.User_Works", "StartDateDay");
        }
    }
}
