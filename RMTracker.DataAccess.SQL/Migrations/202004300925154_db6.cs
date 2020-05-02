namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sub_C2B", "OrderID_Cut", c => c.String());
            AddColumn("dbo.Sub_C2B", "OrderID_EdgeBanding", c => c.String());
            AddColumn("dbo.Sub_C2B", "OrderID_Drill", c => c.String());
            AddColumn("dbo.Sub_C2B", "OrderID_Packing", c => c.String());
            AddColumn("dbo.Sub_C2B", "OrderID_Pickup", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sub_C2B", "OrderID_Pickup");
            DropColumn("dbo.Sub_C2B", "OrderID_Packing");
            DropColumn("dbo.Sub_C2B", "OrderID_Drill");
            DropColumn("dbo.Sub_C2B", "OrderID_EdgeBanding");
            DropColumn("dbo.Sub_C2B", "OrderID_Cut");
        }
    }
}
