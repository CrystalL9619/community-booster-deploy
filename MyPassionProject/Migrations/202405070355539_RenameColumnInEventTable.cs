namespace MyPassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumnInEventTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.ApplicationUserEvents", "Event_EventId", "EventId");

        }
        
        public override void Down()
        {
            RenameColumn("dbo.ApplicationUserEvents", "EventId", "Event_EventId");
        }
    }
}
