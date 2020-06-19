namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Cleaning", "Urgent_Status", c => c.String());
            AddColumn("dbo.s_Cut", "Urgent_Status", c => c.String());
            AddColumn("dbo.s_Drill", "Urgent_Status", c => c.String());
            AddColumn("dbo.s_Edgebanding", "Urgent_Status", c => c.String());
            AddColumn("dbo.s_Lamination", "Urgent_Status", c => c.String());
            AddColumn("dbo.s_Packing", "Urgent_Status", c => c.String());
            AddColumn("dbo.s_Painting", "Urgent_Status", c => c.String());
            AddColumn("dbo.s_Pickup", "Urgent_Status", c => c.String());
            AddColumn("dbo.s_QC", "Urgent_Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.s_QC", "Urgent_Status");
            DropColumn("dbo.s_Pickup", "Urgent_Status");
            DropColumn("dbo.s_Painting", "Urgent_Status");
            DropColumn("dbo.s_Packing", "Urgent_Status");
            DropColumn("dbo.s_Lamination", "Urgent_Status");
            DropColumn("dbo.s_Edgebanding", "Urgent_Status");
            DropColumn("dbo.s_Drill", "Urgent_Status");
            DropColumn("dbo.s_Cut", "Urgent_Status");
            DropColumn("dbo.s_Cleaning", "Urgent_Status");
        }
    }
}
