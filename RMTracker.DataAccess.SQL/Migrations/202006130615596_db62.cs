namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db62 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReasonDenines", "No", c => c.String());
            AddColumn("dbo.ReasonPauses", "No", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReasonPauses", "No");
            DropColumn("dbo.ReasonDenines", "No");
        }
    }
}
