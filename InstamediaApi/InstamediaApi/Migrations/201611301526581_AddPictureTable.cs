namespace InstamediaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPictureTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        UserId = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "UserId", "dbo.Users");
            DropIndex("dbo.Pictures", new[] { "UserId" });
            DropTable("dbo.Pictures");
        }
    }
}
