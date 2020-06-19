namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db24 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SO_PAUSE",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SOID = c.String(),
                        Lamination = c.String(),
                        Cut = c.String(),
                        Edgebamding = c.String(),
                        Drill = c.String(),
                        Painting = c.String(),
                        Cleaning = c.String(),
                        Packing = c.String(),
                        QC = c.String(),
                        Pickup = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sub_C2B", "Order_PauseID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sub_C2B", "Order_PauseID");
            DropTable("dbo.SO_PAUSE");
        }
    }
}
