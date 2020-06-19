namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db41 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sub_C2B", "Cancel_Reason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sub_C2B", "Cancel_Reason");
        }
    }
}
