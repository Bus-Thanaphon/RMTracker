namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db42 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.s_Cleaning", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.s_Cut", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.s_Drill", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.s_Edgebanding", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.s_Lamination", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.s_Packing", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.s_Painting", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.s_Painting", "Quantity", c => c.String());
            AlterColumn("dbo.s_Packing", "Quantity", c => c.String());
            AlterColumn("dbo.s_Lamination", "Quantity", c => c.String());
            AlterColumn("dbo.s_Edgebanding", "Quantity", c => c.String());
            AlterColumn("dbo.s_Drill", "Quantity", c => c.String());
            AlterColumn("dbo.s_Cut", "Quantity", c => c.String());
            AlterColumn("dbo.s_Cleaning", "Quantity", c => c.String());
        }
    }
}
