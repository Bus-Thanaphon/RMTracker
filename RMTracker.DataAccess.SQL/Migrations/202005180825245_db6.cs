namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MachineLists",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ID_Machine = c.String(),
                        Name = c.String(),
                        Department = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLists",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ID_User = c.String(),
                        Name = c.String(),
                        Department1 = c.String(),
                        Department2 = c.String(),
                        Department3 = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserLists");
            DropTable("dbo.MachineLists");
        }
    }
}
