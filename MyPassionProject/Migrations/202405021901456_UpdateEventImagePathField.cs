namespace MyPassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEventImagePathField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "ImagePath", c => c.String());
            DropColumn("dbo.Events", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Image", c => c.Binary());
            DropColumn("dbo.Events", "ImagePath");
        }
    }
}
