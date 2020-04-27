namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sub_C2B",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SubC2B = c.String(),
                        OrderID_Lamination = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User_Works",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        C2BNo = c.String(),
                        StartDate = c.DateTimeOffset(nullable: false, precision: 7),
                        EndDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Current_Station = c.String(),
                        Job_Status = c.String(),
                        Order_Status = c.String(),
                        Comment = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User_Works");
            DropTable("dbo.Sub_C2B");
        }
    }
}
