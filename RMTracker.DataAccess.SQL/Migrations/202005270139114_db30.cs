namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db30 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sub_C2B", "User_Works_Id", "dbo.User_Works");
            AddColumn("dbo.Sub_C2B", "User_Works_Id1", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sub_C2B", "User_Works_Id1");
            AddForeignKey("dbo.Sub_C2B", "User_Works_Id", "dbo.User_Works", "Id");
            AddForeignKey("dbo.Sub_C2B", "User_Works_Id1", "dbo.User_Works", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sub_C2B", "User_Works_Id1", "dbo.User_Works");
            DropForeignKey("dbo.Sub_C2B", "User_Works_Id", "dbo.User_Works");
            DropIndex("dbo.Sub_C2B", new[] { "User_Works_Id1" });
            DropColumn("dbo.Sub_C2B", "User_Works_Id1");
            AddForeignKey("dbo.Sub_C2B", "User_Works_Id", "dbo.User_Works", "Id");
        }
    }
}
