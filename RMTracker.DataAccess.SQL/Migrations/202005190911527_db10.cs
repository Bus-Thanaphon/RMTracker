namespace RMTracker.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departmentusers", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departmentusers", "Number");
        }
    }
}
