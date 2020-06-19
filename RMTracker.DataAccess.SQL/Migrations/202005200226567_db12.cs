namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sub_C2B", "CheckBoxQC", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sub_C2B", "OrderID_QC", c => c.String());
            AddColumn("dbo.Sub_C2B", "Status_QC_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sub_C2B", "Status_QC_Id");
            AddForeignKey("dbo.Sub_C2B", "Status_QC_Id", "dbo.s_Packing", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sub_C2B", "Status_QC_Id", "dbo.s_Packing");
            DropIndex("dbo.Sub_C2B", new[] { "Status_QC_Id" });
            DropColumn("dbo.Sub_C2B", "Status_QC_Id");
            DropColumn("dbo.Sub_C2B", "OrderID_QC");
            DropColumn("dbo.Sub_C2B", "CheckBoxQC");
        }
    }
}
