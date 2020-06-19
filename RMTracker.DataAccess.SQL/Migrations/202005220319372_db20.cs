namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Cleaning", "Status_Check", c => c.Boolean(nullable: false));
            AddColumn("dbo.s_Cut", "Status_Check", c => c.Boolean(nullable: false));
            AddColumn("dbo.s_Drill", "Status_Check", c => c.Boolean(nullable: false));
            AddColumn("dbo.s_Edgebanding", "Status_Check", c => c.Boolean(nullable: false));
            AddColumn("dbo.s_Lamination", "Status_Check", c => c.Boolean(nullable: false));
            AddColumn("dbo.s_Packing", "Status_Check", c => c.Boolean(nullable: false));
            AddColumn("dbo.s_Painting", "Status_Check", c => c.Boolean(nullable: false));
            AddColumn("dbo.s_Pickup", "Status_Check", c => c.Boolean(nullable: false));
            AddColumn("dbo.s_QC", "Status_Check", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.s_QC", "Status_Check");
            DropColumn("dbo.s_Pickup", "Status_Check");
            DropColumn("dbo.s_Painting", "Status_Check");
            DropColumn("dbo.s_Packing", "Status_Check");
            DropColumn("dbo.s_Lamination", "Status_Check");
            DropColumn("dbo.s_Edgebanding", "Status_Check");
            DropColumn("dbo.s_Drill", "Status_Check");
            DropColumn("dbo.s_Cut", "Status_Check");
            DropColumn("dbo.s_Cleaning", "Status_Check");
        }
    }
}
