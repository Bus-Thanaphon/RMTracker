namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.s_Lamination", "Sub_C2B_Id", "dbo.Sub_C2B");
            DropIndex("dbo.s_Lamination", new[] { "Sub_C2B_Id" });
            AddColumn("dbo.Sub_C2B", "Status_lamination_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sub_C2B", "Status_lamination_Id");
            AddForeignKey("dbo.Sub_C2B", "Status_lamination_Id", "dbo.s_Lamination", "Id");
            DropColumn("dbo.s_Lamination", "Sub_C2B_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.s_Lamination", "Sub_C2B_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Sub_C2B", "Status_lamination_Id", "dbo.s_Lamination");
            DropIndex("dbo.Sub_C2B", new[] { "Status_lamination_Id" });
            DropColumn("dbo.Sub_C2B", "Status_lamination_Id");
            CreateIndex("dbo.s_Lamination", "Sub_C2B_Id");
            AddForeignKey("dbo.s_Lamination", "Sub_C2B_Id", "dbo.Sub_C2B", "Id");
        }
    }
}
