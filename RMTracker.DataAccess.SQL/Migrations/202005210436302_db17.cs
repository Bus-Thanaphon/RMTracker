namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Cut", "Time_pauses", c => c.Int(nullable: false));
            AddColumn("dbo.s_Cut", "PauseID", c => c.String());
            AddColumn("dbo.s_Cut", "User", c => c.String());
            AddColumn("dbo.s_Cut", "Start_Time", c => c.String());
            AddColumn("dbo.s_Cut", "End_Time", c => c.String());
            DropColumn("dbo.s_Cut", "Time_pause");
        }
        
        public override void Down()
        {
            AddColumn("dbo.s_Cut", "Time_pause", c => c.String());
            DropColumn("dbo.s_Cut", "End_Time");
            DropColumn("dbo.s_Cut", "Start_Time");
            DropColumn("dbo.s_Cut", "User");
            DropColumn("dbo.s_Cut", "PauseID");
            DropColumn("dbo.s_Cut", "Time_pauses");
        }
    }
}
