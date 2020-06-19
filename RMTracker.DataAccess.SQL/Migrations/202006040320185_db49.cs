namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db49 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User_Works", "C2BNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User_Works", "C2BNo", c => c.String(nullable: false));
        }
    }
}
