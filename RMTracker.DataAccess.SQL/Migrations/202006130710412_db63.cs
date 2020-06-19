namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db63 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorksDenines", "Other_Reason", c => c.String());
            AddColumn("dbo.WorksPauses", "Other_Reason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorksPauses", "Other_Reason");
            DropColumn("dbo.WorksDenines", "Other_Reason");
        }
    }
}
