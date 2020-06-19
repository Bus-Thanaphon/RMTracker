namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db57 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sub_C2B", "StartSDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sub_C2B", "StartSDate", c => c.DateTime(nullable: false));
        }
    }
}
