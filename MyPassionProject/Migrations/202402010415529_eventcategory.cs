namespace MyPassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventcategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "CategoryId");
            AddForeignKey("dbo.Events", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Events", new[] { "CategoryId" });
            DropColumn("dbo.Events", "CategoryId");
        }
    }
}
