namespace InstamediaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostsTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "UserId");
        }
    }
}
