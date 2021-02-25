namespace VGS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        GameName = c.String(nullable: false),
                        Genre = c.String(nullable: false),
                        StudioId = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        ImagePath = c.String(nullable: false),
                        AverageRating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Studios", t => t.StudioId, cascadeDelete: true)
                .Index(t => t.StudioId);
            
            CreateTable(
                "dbo.Studios",
                c => new
                    {
                        StudioId = c.Int(nullable: false, identity: true),
                        StudioName = c.String(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longtitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StudioId);
            
            CreateTable(
                "dbo.UserGames",
                c => new
                    {
                        UserGameId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserGameId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        Balance = c.Double(nullable: false),
                        Password = c.String(nullable: false),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
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
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "GameId", "dbo.Games");
            DropForeignKey("dbo.UserGames", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "StudioId", "dbo.Studios");
            DropIndex("dbo.Reviews", new[] { "GameId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.UserGames", new[] { "GameId" });
            DropIndex("dbo.UserGames", new[] { "UserId" });
            DropIndex("dbo.Games", new[] { "StudioId" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Users");
            DropTable("dbo.UserGames");
            DropTable("dbo.Studios");
            DropTable("dbo.Games");
        }
    }
}
