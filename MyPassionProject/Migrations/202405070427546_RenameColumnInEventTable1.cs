namespace MyPassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumnInEventTable1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.ApplicationUserEvents", "EventId", "Event_EventId");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.ApplicationUserEvents", "Event_EventId", "EventId");
        }
    }
}
