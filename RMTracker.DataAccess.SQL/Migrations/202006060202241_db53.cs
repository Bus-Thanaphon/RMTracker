namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db53 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MachineLists", "NickName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MachineLists", "NickName");
        }
    }
}
