namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db50 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.s_Edgebanding", "Quantity", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.s_Edgebanding", "Quantity", c => c.Int(nullable: false));
        }
    }
}
