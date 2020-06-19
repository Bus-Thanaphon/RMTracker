namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db35 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Cleaning", "Denine_Check", c => c.String());
            AddColumn("dbo.s_Cut", "Denine_Check", c => c.String());
            AddColumn("dbo.s_Drill", "Denine_Check", c => c.String());
            AddColumn("dbo.s_Edgebanding", "Denine_Check", c => c.String());
            AddColumn("dbo.s_Lamination", "Denine_Check", c => c.String());
            AddColumn("dbo.s_Packing", "Denine_Check", c => c.String());
            AddColumn("dbo.s_Painting", "Denine_Check", c => c.String());
            AddColumn("dbo.s_Pickup", "Denine_Check", c => c.String());
            AddColumn("dbo.s_QC", "Denine_Check", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.s_QC", "Denine_Check");
            DropColumn("dbo.s_Pickup", "Denine_Check");
            DropColumn("dbo.s_Painting", "Denine_Check");
            DropColumn("dbo.s_Packing", "Denine_Check");
            DropColumn("dbo.s_Lamination", "Denine_Check");
            DropColumn("dbo.s_Edgebanding", "Denine_Check");
            DropColumn("dbo.s_Drill", "Denine_Check");
            DropColumn("dbo.s_Cut", "Denine_Check");
            DropColumn("dbo.s_Cleaning", "Denine_Check");
        }
    }
}
