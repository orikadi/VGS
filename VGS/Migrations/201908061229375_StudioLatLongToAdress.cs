namespace VGS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudioLatLongToAdress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Studios", "Address", c => c.String(nullable: false));
            DropColumn("dbo.Studios", "Latitude");
            DropColumn("dbo.Studios", "Longtitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Studios", "Longtitude", c => c.Double(nullable: false));
            AddColumn("dbo.Studios", "Latitude", c => c.Double(nullable: false));
            DropColumn("dbo.Studios", "Address");
        }
    }
}
