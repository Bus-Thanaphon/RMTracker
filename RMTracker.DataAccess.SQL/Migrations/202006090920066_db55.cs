namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db55 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Packing", "Machine_Pack", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.s_Packing", "Machine_Pack");
        }
    }
}
