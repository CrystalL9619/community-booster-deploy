namespace MyPassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAspNetUserNEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventAppUsers", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.EventAppUsers", "AppUser_UserId", "dbo.AppUsers");
            DropIndex("dbo.EventAppUsers", new[] { "Event_EventId" });
            DropIndex("dbo.EventAppUsers", new[] { "AppUser_UserId" });
            CreateTable(
                "dbo.ApplicationUserEvents",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Event_EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Event_EventId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_EventId, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Event_EventId);
            
            DropTable("dbo.EventAppUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EventAppUsers",
                c => new
                    {
                        Event_EventId = c.Int(nullable: false),
                        AppUser_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_EventId, t.AppUser_UserId });
            
            DropForeignKey("dbo.ApplicationUserEvents", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.ApplicationUserEvents", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserEvents", new[] { "Event_EventId" });
            DropIndex("dbo.ApplicationUserEvents", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserEvents");
            CreateIndex("dbo.EventAppUsers", "AppUser_UserId");
            CreateIndex("dbo.EventAppUsers", "Event_EventId");
            AddForeignKey("dbo.EventAppUsers", "AppUser_UserId", "dbo.AppUsers", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.EventAppUsers", "Event_EventId", "dbo.Events", "EventId", cascadeDelete: true);
        }
    }
}
