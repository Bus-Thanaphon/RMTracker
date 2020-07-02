namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db64 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Cleaning", "Duedate", c => c.String());
            AddColumn("dbo.s_Cleaning", "NameSale", c => c.String());
            AddColumn("dbo.s_Cut", "Duedate", c => c.String());
            AddColumn("dbo.s_Cut", "NameSale", c => c.String());
            AddColumn("dbo.s_Drill", "Duedate", c => c.String());
            AddColumn("dbo.s_Drill", "NameSale", c => c.String());
            AddColumn("dbo.s_Edgebanding", "Duedate", c => c.String());
            AddColumn("dbo.s_Edgebanding", "NameSale", c => c.String());
            AddColumn("dbo.s_Lamination", "Duedate", c => c.String());
            AddColumn("dbo.s_Lamination", "NameSale", c => c.String());
            AddColumn("dbo.s_Packing", "Duedate", c => c.String());
            AddColumn("dbo.s_Packing", "NameSale", c => c.String());
            AddColumn("dbo.s_Painting", "Duedate", c => c.String());
            AddColumn("dbo.s_Painting", "NameSale", c => c.String());
            AddColumn("dbo.s_Pickup", "Duedate", c => c.String());
            AddColumn("dbo.s_Pickup", "NameSale", c => c.String());
            AddColumn("dbo.s_QC", "Duedate", c => c.String());
            AddColumn("dbo.s_QC", "NameSale", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.s_QC", "NameSale");
            DropColumn("dbo.s_QC", "Duedate");
            DropColumn("dbo.s_Pickup", "NameSale");
            DropColumn("dbo.s_Pickup", "Duedate");
            DropColumn("dbo.s_Painting", "NameSale");
            DropColumn("dbo.s_Painting", "Duedate");
            DropColumn("dbo.s_Packing", "NameSale");
            DropColumn("dbo.s_Packing", "Duedate");
            DropColumn("dbo.s_Lamination", "NameSale");
            DropColumn("dbo.s_Lamination", "Duedate");
            DropColumn("dbo.s_Edgebanding", "NameSale");
            DropColumn("dbo.s_Edgebanding", "Duedate");
            DropColumn("dbo.s_Drill", "NameSale");
            DropColumn("dbo.s_Drill", "Duedate");
            DropColumn("dbo.s_Cut", "NameSale");
            DropColumn("dbo.s_Cut", "Duedate");
            DropColumn("dbo.s_Cleaning", "NameSale");
            DropColumn("dbo.s_Cleaning", "Duedate");
        }
    }
}
