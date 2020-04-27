namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.On_Hold");
            DropPrimaryKey("dbo.Sub_C2B");
            DropPrimaryKey("dbo.User_Works");
            AlterColumn("dbo.On_Hold", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Sub_C2B", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.User_Works", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.On_Hold", "Id");
            AddPrimaryKey("dbo.Sub_C2B", "Id");
            AddPrimaryKey("dbo.User_Works", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.User_Works");
            DropPrimaryKey("dbo.Sub_C2B");
            DropPrimaryKey("dbo.On_Hold");
            AlterColumn("dbo.User_Works", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Sub_C2B", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.On_Hold", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.User_Works", "Id");
            AddPrimaryKey("dbo.Sub_C2B", "Id");
            AddPrimaryKey("dbo.On_Hold", "Id");
        }
    }
}
