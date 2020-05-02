namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sub_C2B", "User_Works_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sub_C2B", "User_Works_Id");
            AddForeignKey("dbo.Sub_C2B", "User_Works_Id", "dbo.User_Works", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sub_C2B", "User_Works_Id", "dbo.User_Works");
            DropIndex("dbo.Sub_C2B", new[] { "User_Works_Id" });
            DropColumn("dbo.Sub_C2B", "User_Works_Id");
        }
    }
}
