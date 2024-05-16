namespace MyPassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class img : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Image", c => c.Binary());
            DropColumn("dbo.Events", "EventHasPic");
          
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "PicExtension", c => c.String());
            AddColumn("dbo.Events", "EventHasPic", c => c.Boolean(nullable: false));
            DropColumn("dbo.Events", "Image");
        }
    }
}
