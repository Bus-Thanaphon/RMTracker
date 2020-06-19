namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorksPauses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        StationID = c.String(),
                        Start_Time = c.String(),
                        End_Time = c.String(),
                        Reason = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.s_Lamination", "Time_pauses", c => c.Int(nullable: false));
            DropColumn("dbo.s_Lamination", "Time_pause");
        }
        
        public override void Down()
        {
            AddColumn("dbo.s_Lamination", "Time_pause", c => c.String());
            DropColumn("dbo.s_Lamination", "Time_pauses");
            DropTable("dbo.WorksPauses");
        }
    }
}
