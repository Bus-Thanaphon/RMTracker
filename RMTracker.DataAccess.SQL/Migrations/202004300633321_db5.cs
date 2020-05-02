namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User_Works", "StartDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User_Works", "StartDate", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
