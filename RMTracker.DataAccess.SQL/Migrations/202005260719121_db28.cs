﻿namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db28 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Works", "Status_Sub_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.User_Works", "Status_Sub_Id");
            AddForeignKey("dbo.User_Works", "Status_Sub_Id", "dbo.Sub_C2B", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Works", "Status_Sub_Id", "dbo.Sub_C2B");
            DropIndex("dbo.User_Works", new[] { "Status_Sub_Id" });
            DropColumn("dbo.User_Works", "Status_Sub_Id");
        }
    }
}
