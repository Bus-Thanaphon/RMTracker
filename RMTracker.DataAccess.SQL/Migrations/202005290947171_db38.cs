namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db38 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sub_C2B", "StartDate", c => c.String());
            AddColumn("dbo.Sub_C2B", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sub_C2B", "Comment");
            DropColumn("dbo.Sub_C2B", "StartDate");
        }
    }
}
