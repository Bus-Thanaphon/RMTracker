namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Cut", "DenineID", c => c.String());
            AddColumn("dbo.s_Edgebanding", "Time_pauses", c => c.Int(nullable: false));
            AddColumn("dbo.s_Edgebanding", "PauseID", c => c.String());
            AddColumn("dbo.s_Edgebanding", "User", c => c.String());
            AddColumn("dbo.s_Edgebanding", "Start_Time", c => c.String());
            AddColumn("dbo.s_Edgebanding", "End_Time", c => c.String());
            AddColumn("dbo.s_Edgebanding", "DenineID", c => c.String());
            DropColumn("dbo.s_Edgebanding", "Time_pause");
        }
        
        public override void Down()
        {
            AddColumn("dbo.s_Edgebanding", "Time_pause", c => c.String());
            DropColumn("dbo.s_Edgebanding", "DenineID");
            DropColumn("dbo.s_Edgebanding", "End_Time");
            DropColumn("dbo.s_Edgebanding", "Start_Time");
            DropColumn("dbo.s_Edgebanding", "User");
            DropColumn("dbo.s_Edgebanding", "PauseID");
            DropColumn("dbo.s_Edgebanding", "Time_pauses");
            DropColumn("dbo.s_Cut", "DenineID");
        }
    }
}
