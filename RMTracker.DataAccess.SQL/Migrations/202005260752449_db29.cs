namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db29 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User_Works", "Status_Sub_Id", "dbo.Sub_C2B");
            DropIndex("dbo.User_Works", new[] { "Status_Sub_Id" });
            DropColumn("dbo.User_Works", "Status_Sub_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_Works", "Status_Sub_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.User_Works", "Status_Sub_Id");
            AddForeignKey("dbo.User_Works", "Status_Sub_Id", "dbo.Sub_C2B", "Id");
        }
    }
}
