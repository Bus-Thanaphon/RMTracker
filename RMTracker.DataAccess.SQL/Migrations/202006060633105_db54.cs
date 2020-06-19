namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db54 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLists", "Fullname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLists", "Fullname");
        }
    }
}
