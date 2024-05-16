namespace MyPassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Event1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "UpdateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Events", "EventDateTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "EventDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Events", "UpdateDate", c => c.DateTime(nullable: false));
        }
    }
}
