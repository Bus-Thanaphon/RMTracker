namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db46 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sub_C2B", "Previousstatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sub_C2B", "Previousstatus");
        }
    }
}
