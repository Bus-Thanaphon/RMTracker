namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db59 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Cleaning", "Customer", c => c.String());
            AddColumn("dbo.s_Cut", "Customer", c => c.String());
            AddColumn("dbo.s_Drill", "Customer", c => c.String());
            AddColumn("dbo.s_Edgebanding", "Customer", c => c.String());
            AddColumn("dbo.s_Lamination", "Customer", c => c.String());
            AddColumn("dbo.s_Packing", "Customer", c => c.String());
            AddColumn("dbo.s_Painting", "Customer", c => c.String());
            AddColumn("dbo.s_Pickup", "Customer", c => c.String());
            AddColumn("dbo.s_QC", "Customer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.s_QC", "Customer");
            DropColumn("dbo.s_Pickup", "Customer");
            DropColumn("dbo.s_Painting", "Customer");
            DropColumn("dbo.s_Packing", "Customer");
            DropColumn("dbo.s_Lamination", "Customer");
            DropColumn("dbo.s_Edgebanding", "Customer");
            DropColumn("dbo.s_Drill", "Customer");
            DropColumn("dbo.s_Cut", "Customer");
            DropColumn("dbo.s_Cleaning", "Customer");
        }
    }
}
