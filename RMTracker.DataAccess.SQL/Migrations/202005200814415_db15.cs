namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorksPauses", "SONO", c => c.String());
            AddColumn("dbo.WorksPauses", "Station", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorksPauses", "Station");
            DropColumn("dbo.WorksPauses", "SONO");
        }
    }
}
