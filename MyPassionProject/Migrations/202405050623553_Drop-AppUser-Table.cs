namespace MyPassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropAppUserTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.AppUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
    }
}
