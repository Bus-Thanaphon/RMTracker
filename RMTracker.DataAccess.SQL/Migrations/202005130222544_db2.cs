namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Sub_C2B", new[] { "Status_lamination_Id" });
            AddColumn("dbo.Sub_C2B", "Status_Cleaning_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Sub_C2B", "Status_Cut_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Sub_C2B", "Status_Drill_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Sub_C2B", "Status_EdgeBanding_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Sub_C2B", "Status_Packing_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Sub_C2B", "Status_Painting_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Sub_C2B", "Status_Pickup_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sub_C2B", "Status_Cleaning_Id");
            CreateIndex("dbo.Sub_C2B", "Status_Cut_Id");
            CreateIndex("dbo.Sub_C2B", "Status_Drill_Id");
            CreateIndex("dbo.Sub_C2B", "Status_EdgeBanding_Id");
            CreateIndex("dbo.Sub_C2B", "Status_Lamination_Id");
            CreateIndex("dbo.Sub_C2B", "Status_Packing_Id");
            CreateIndex("dbo.Sub_C2B", "Status_Painting_Id");
            CreateIndex("dbo.Sub_C2B", "Status_Pickup_Id");
            AddForeignKey("dbo.Sub_C2B", "Status_Cleaning_Id", "dbo.s_Cleaning", "Id");
            AddForeignKey("dbo.Sub_C2B", "Status_Cut_Id", "dbo.s_Cut", "Id");
            AddForeignKey("dbo.Sub_C2B", "Status_Drill_Id", "dbo.s_Drill", "Id");
            AddForeignKey("dbo.Sub_C2B", "Status_EdgeBanding_Id", "dbo.s_Edgebanding", "Id");
            AddForeignKey("dbo.Sub_C2B", "Status_Packing_Id", "dbo.s_Packing", "Id");
            AddForeignKey("dbo.Sub_C2B", "Status_Painting_Id", "dbo.s_Painting", "Id");
            AddForeignKey("dbo.Sub_C2B", "Status_Pickup_Id", "dbo.s_Pickup", "Id");
            DropColumn("dbo.Sub_C2B", "Status_Cut");
            DropColumn("dbo.Sub_C2B", "Status_EdgeBanding");
            DropColumn("dbo.Sub_C2B", "Status_Drill");
            DropColumn("dbo.Sub_C2B", "Status_Painting");
            DropColumn("dbo.Sub_C2B", "Status_Cleaning");
            DropColumn("dbo.Sub_C2B", "Status_Packing");
            DropColumn("dbo.Sub_C2B", "Status_Pickup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sub_C2B", "Status_Pickup", c => c.String());
            AddColumn("dbo.Sub_C2B", "Status_Packing", c => c.String());
            AddColumn("dbo.Sub_C2B", "Status_Cleaning", c => c.String());
            AddColumn("dbo.Sub_C2B", "Status_Painting", c => c.String());
            AddColumn("dbo.Sub_C2B", "Status_Drill", c => c.String());
            AddColumn("dbo.Sub_C2B", "Status_EdgeBanding", c => c.String());
            AddColumn("dbo.Sub_C2B", "Status_Cut", c => c.String());
            DropForeignKey("dbo.Sub_C2B", "Status_Pickup_Id", "dbo.s_Pickup");
            DropForeignKey("dbo.Sub_C2B", "Status_Painting_Id", "dbo.s_Painting");
            DropForeignKey("dbo.Sub_C2B", "Status_Packing_Id", "dbo.s_Packing");
            DropForeignKey("dbo.Sub_C2B", "Status_EdgeBanding_Id", "dbo.s_Edgebanding");
            DropForeignKey("dbo.Sub_C2B", "Status_Drill_Id", "dbo.s_Drill");
            DropForeignKey("dbo.Sub_C2B", "Status_Cut_Id", "dbo.s_Cut");
            DropForeignKey("dbo.Sub_C2B", "Status_Cleaning_Id", "dbo.s_Cleaning");
            DropIndex("dbo.Sub_C2B", new[] { "Status_Pickup_Id" });
            DropIndex("dbo.Sub_C2B", new[] { "Status_Painting_Id" });
            DropIndex("dbo.Sub_C2B", new[] { "Status_Packing_Id" });
            DropIndex("dbo.Sub_C2B", new[] { "Status_Lamination_Id" });
            DropIndex("dbo.Sub_C2B", new[] { "Status_EdgeBanding_Id" });
            DropIndex("dbo.Sub_C2B", new[] { "Status_Drill_Id" });
            DropIndex("dbo.Sub_C2B", new[] { "Status_Cut_Id" });
            DropIndex("dbo.Sub_C2B", new[] { "Status_Cleaning_Id" });
            DropColumn("dbo.Sub_C2B", "Status_Pickup_Id");
            DropColumn("dbo.Sub_C2B", "Status_Painting_Id");
            DropColumn("dbo.Sub_C2B", "Status_Packing_Id");
            DropColumn("dbo.Sub_C2B", "Status_EdgeBanding_Id");
            DropColumn("dbo.Sub_C2B", "Status_Drill_Id");
            DropColumn("dbo.Sub_C2B", "Status_Cut_Id");
            DropColumn("dbo.Sub_C2B", "Status_Cleaning_Id");
            CreateIndex("dbo.Sub_C2B", "Status_lamination_Id");
        }
    }
}
