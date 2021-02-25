namespace VGS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedReviewModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "GameId", "dbo.Games");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "GameId" });
            DropTable("dbo.Reviews");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        Text = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId);
            
            CreateIndex("dbo.Reviews", "GameId");
            CreateIndex("dbo.Reviews", "UserId");
            AddForeignKey("dbo.Reviews", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "GameId", "dbo.Games", "GameId", cascadeDelete: true);
        }
    }
}
