namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db56 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sub_C2B", "StartSDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sub_C2B", "StartDateDay", c => c.Int(nullable: false));
            AddColumn("dbo.Sub_C2B", "StartDateMonth", c => c.Int(nullable: false));
            AddColumn("dbo.Sub_C2B", "EndSDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sub_C2B", "EndDateDay", c => c.Int(nullable: false));
            AddColumn("dbo.Sub_C2B", "EndDateMonth", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sub_C2B", "EndDateMonth");
            DropColumn("dbo.Sub_C2B", "EndDateDay");
            DropColumn("dbo.Sub_C2B", "EndSDate");
            DropColumn("dbo.Sub_C2B", "StartDateMonth");
            DropColumn("dbo.Sub_C2B", "StartDateDay");
            DropColumn("dbo.Sub_C2B", "StartSDate");
        }
    }
}
