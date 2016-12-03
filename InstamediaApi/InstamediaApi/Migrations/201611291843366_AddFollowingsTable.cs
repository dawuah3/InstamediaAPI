namespace InstamediaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowingsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        FollowerId = c.String(nullable: false, maxLength: 128),
                        FolloweeId = c.String(nullable: false, maxLength: 128),
                        Followee_Id = c.Int(),
                        Follower_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FolloweeId })
                .ForeignKey("dbo.Users", t => t.Followee_Id)
                .ForeignKey("dbo.Users", t => t.Follower_Id)
                .Index(t => t.Followee_Id)
                .Index(t => t.Follower_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "Follower_Id", "dbo.Users");
            DropForeignKey("dbo.Followings", "Followee_Id", "dbo.Users");
            DropIndex("dbo.Followings", new[] { "Follower_Id" });
            DropIndex("dbo.Followings", new[] { "Followee_Id" });
            DropTable("dbo.Followings");
        }
    }
}
