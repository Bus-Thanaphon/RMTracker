namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Cleaning", "Quantity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.s_Cleaning", "Quantity");
        }
    }
}
