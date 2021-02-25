namespace VGS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAverageRatingToRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Rating", c => c.Double(nullable: false));
            DropColumn("dbo.Games", "AverageRating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "AverageRating", c => c.Double(nullable: false));
            DropColumn("dbo.Games", "Rating");
        }
    }
}
