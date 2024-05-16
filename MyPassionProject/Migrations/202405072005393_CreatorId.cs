namespace MyPassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatorId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "CreatorId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "CreatorId");
        }
    }
}
