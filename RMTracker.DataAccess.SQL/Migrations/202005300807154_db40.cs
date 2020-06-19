namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db40 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorksDenines", "StartDate", c => c.String());
            AddColumn("dbo.WorksDenines", "EndDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorksDenines", "EndDate");
            DropColumn("dbo.WorksDenines", "StartDate");
        }
    }
}
