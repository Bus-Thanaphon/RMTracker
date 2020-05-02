namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sub_C2B", "countno", c => c.Int(nullable: false));
            AddColumn("dbo.User_Works", "SubC2B", c => c.Int(nullable: false));
            DropColumn("dbo.User_Works", "Current_Station");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_Works", "Current_Station", c => c.String());
            DropColumn("dbo.User_Works", "SubC2B");
            DropColumn("dbo.Sub_C2B", "countno");
        }
    }
}
