namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Cleaning", "Status_Show", c => c.String());
            AddColumn("dbo.s_Cut", "Status_Show", c => c.String());
            AddColumn("dbo.s_Drill", "Status_Show", c => c.String());
            AddColumn("dbo.s_Edgebanding", "Status_Show", c => c.String());
            AddColumn("dbo.s_Lamination", "Status_Show", c => c.String());
            AddColumn("dbo.s_Packing", "Status_Show", c => c.String());
            AddColumn("dbo.s_Painting", "Status_Show", c => c.String());
            AddColumn("dbo.s_Pickup", "Status_Show", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.s_Pickup", "Status_Show");
            DropColumn("dbo.s_Painting", "Status_Show");
            DropColumn("dbo.s_Packing", "Status_Show");
            DropColumn("dbo.s_Lamination", "Status_Show");
            DropColumn("dbo.s_Edgebanding", "Status_Show");
            DropColumn("dbo.s_Drill", "Status_Show");
            DropColumn("dbo.s_Cut", "Status_Show");
            DropColumn("dbo.s_Cleaning", "Status_Show");
        }
    }
}
