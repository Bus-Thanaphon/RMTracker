namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s_Cleaning", "Time_pauses", c => c.Int(nullable: false));
            AddColumn("dbo.s_Cleaning", "PauseID", c => c.String());
            AddColumn("dbo.s_Cleaning", "User", c => c.String());
            AddColumn("dbo.s_Cleaning", "Start_Time", c => c.String());
            AddColumn("dbo.s_Cleaning", "End_Time", c => c.String());
            AddColumn("dbo.s_Cleaning", "DenineID", c => c.String());
            AddColumn("dbo.s_Drill", "Time_pauses", c => c.Int(nullable: false));
            AddColumn("dbo.s_Drill", "PauseID", c => c.String());
            AddColumn("dbo.s_Drill", "User", c => c.String());
            AddColumn("dbo.s_Drill", "Start_Time", c => c.String());
            AddColumn("dbo.s_Drill", "End_Time", c => c.String());
            AddColumn("dbo.s_Drill", "DenineID", c => c.String());
            AddColumn("dbo.s_Packing", "Time_pauses", c => c.Int(nullable: false));
            AddColumn("dbo.s_Packing", "PauseID", c => c.String());
            AddColumn("dbo.s_Packing", "User", c => c.String());
            AddColumn("dbo.s_Packing", "Start_Time", c => c.String());
            AddColumn("dbo.s_Packing", "End_Time", c => c.String());
            AddColumn("dbo.s_Packing", "DenineID", c => c.String());
            AddColumn("dbo.s_Painting", "Time_pauses", c => c.Int(nullable: false));
            AddColumn("dbo.s_Painting", "PauseID", c => c.String());
            AddColumn("dbo.s_Painting", "User", c => c.String());
            AddColumn("dbo.s_Painting", "Start_Time", c => c.String());
            AddColumn("dbo.s_Painting", "End_Time", c => c.String());
            AddColumn("dbo.s_Painting", "DenineID", c => c.String());
            AddColumn("dbo.s_Pickup", "Time_pauses", c => c.Int(nullable: false));
            AddColumn("dbo.s_Pickup", "PauseID", c => c.String());
            AddColumn("dbo.s_Pickup", "User", c => c.String());
            AddColumn("dbo.s_Pickup", "Start_Time", c => c.String());
            AddColumn("dbo.s_Pickup", "End_Time", c => c.String());
            AddColumn("dbo.s_Pickup", "DenineID", c => c.String());
            AddColumn("dbo.s_QC", "Time_pauses", c => c.Int(nullable: false));
            AddColumn("dbo.s_QC", "PauseID", c => c.String());
            AddColumn("dbo.s_QC", "Start_Time", c => c.String());
            AddColumn("dbo.s_QC", "End_Time", c => c.String());
            AddColumn("dbo.s_QC", "DenineID", c => c.String());
            DropColumn("dbo.s_Cleaning", "Time_pause");
            DropColumn("dbo.s_Drill", "Time_pause");
            DropColumn("dbo.s_Packing", "Time_pause");
            DropColumn("dbo.s_Painting", "Time_pause");
            DropColumn("dbo.s_Pickup", "Time_pause");
            DropColumn("dbo.s_QC", "Time_pause");
        }
        
        public override void Down()
        {
            AddColumn("dbo.s_QC", "Time_pause", c => c.String());
            AddColumn("dbo.s_Pickup", "Time_pause", c => c.String());
            AddColumn("dbo.s_Painting", "Time_pause", c => c.String());
            AddColumn("dbo.s_Packing", "Time_pause", c => c.String());
            AddColumn("dbo.s_Drill", "Time_pause", c => c.String());
            AddColumn("dbo.s_Cleaning", "Time_pause", c => c.String());
            DropColumn("dbo.s_QC", "DenineID");
            DropColumn("dbo.s_QC", "End_Time");
            DropColumn("dbo.s_QC", "Start_Time");
            DropColumn("dbo.s_QC", "PauseID");
            DropColumn("dbo.s_QC", "Time_pauses");
            DropColumn("dbo.s_Pickup", "DenineID");
            DropColumn("dbo.s_Pickup", "End_Time");
            DropColumn("dbo.s_Pickup", "Start_Time");
            DropColumn("dbo.s_Pickup", "User");
            DropColumn("dbo.s_Pickup", "PauseID");
            DropColumn("dbo.s_Pickup", "Time_pauses");
            DropColumn("dbo.s_Painting", "DenineID");
            DropColumn("dbo.s_Painting", "End_Time");
            DropColumn("dbo.s_Painting", "Start_Time");
            DropColumn("dbo.s_Painting", "User");
            DropColumn("dbo.s_Painting", "PauseID");
            DropColumn("dbo.s_Painting", "Time_pauses");
            DropColumn("dbo.s_Packing", "DenineID");
            DropColumn("dbo.s_Packing", "End_Time");
            DropColumn("dbo.s_Packing", "Start_Time");
            DropColumn("dbo.s_Packing", "User");
            DropColumn("dbo.s_Packing", "PauseID");
            DropColumn("dbo.s_Packing", "Time_pauses");
            DropColumn("dbo.s_Drill", "DenineID");
            DropColumn("dbo.s_Drill", "End_Time");
            DropColumn("dbo.s_Drill", "Start_Time");
            DropColumn("dbo.s_Drill", "User");
            DropColumn("dbo.s_Drill", "PauseID");
            DropColumn("dbo.s_Drill", "Time_pauses");
            DropColumn("dbo.s_Cleaning", "DenineID");
            DropColumn("dbo.s_Cleaning", "End_Time");
            DropColumn("dbo.s_Cleaning", "Start_Time");
            DropColumn("dbo.s_Cleaning", "User");
            DropColumn("dbo.s_Cleaning", "PauseID");
            DropColumn("dbo.s_Cleaning", "Time_pauses");
        }
    }
}
