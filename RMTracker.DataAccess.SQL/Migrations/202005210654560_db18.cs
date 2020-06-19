namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db18 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorksDenines",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SONO = c.String(),
                        Station = c.String(),
                        Reason = c.String(),
                        CheckBoxDetail = c.Boolean(nullable: false),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.s_Lamination", "DenineID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.s_Lamination", "DenineID");
            DropTable("dbo.WorksDenines");
        }
    }
}
