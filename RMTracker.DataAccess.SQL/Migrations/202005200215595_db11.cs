namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.s_QC",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Id_QC = c.String(),
                        C2BNo = c.String(),
                        SubC2B = c.String(),
                        Time_pause = c.String(),
                        Comment = c.String(),
                        User = c.String(),
                        Special_status = c.String(),
                        Status_QC = c.String(),
                        Status_Show = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.s_QC");
        }
    }
}
