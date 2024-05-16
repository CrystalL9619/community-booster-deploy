namespace MyPassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventHasPic", c => c.Boolean(nullable: false));
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "AnimalHasPic", c => c.Boolean(nullable: false));
            DropColumn("dbo.Events", "EventHasPic");
        }
    }
}
