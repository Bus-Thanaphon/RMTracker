namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorksDenines", "StationID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorksDenines", "StationID");
        }
    }
}
