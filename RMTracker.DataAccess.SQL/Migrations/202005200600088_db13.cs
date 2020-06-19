namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Lamination", "Start_Time", c => c.String());
            AddColumn("dbo.s_Lamination", "End_Time", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.s_Lamination", "End_Time");
            DropColumn("dbo.s_Lamination", "Start_Time");
        }
    }
}
