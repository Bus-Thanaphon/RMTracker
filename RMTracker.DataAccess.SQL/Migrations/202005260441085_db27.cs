namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db27 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sub_C2B", "Urgent_Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sub_C2B", "Urgent_Status");
        }
    }
}
