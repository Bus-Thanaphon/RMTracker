namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db45 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Works", "OnholdID", c => c.String());
            AddColumn("dbo.User_Works", "PreviousStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User_Works", "PreviousStatus");
            DropColumn("dbo.User_Works", "OnholdID");
        }
    }
}
