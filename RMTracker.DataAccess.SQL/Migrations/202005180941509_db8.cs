namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Lamination", "User", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.s_Lamination", "User");
        }
    }
}
