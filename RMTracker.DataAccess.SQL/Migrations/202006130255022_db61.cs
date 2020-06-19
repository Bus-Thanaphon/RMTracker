namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db61 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReasonDenines",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Reason = c.String(),
                        Station = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReasonPauses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Reason = c.String(),
                        Station = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReasonPauses");
            DropTable("dbo.ReasonDenines");
        }
    }
}
