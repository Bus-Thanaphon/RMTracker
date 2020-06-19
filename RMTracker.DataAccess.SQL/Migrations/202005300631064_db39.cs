namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db39 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sub_C2B", "EndtDate", c => c.String());
            AddColumn("dbo.Sub_C2B", "CountSend", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sub_C2B", "CountSend");
            DropColumn("dbo.Sub_C2B", "EndtDate");
        }
    }
}
