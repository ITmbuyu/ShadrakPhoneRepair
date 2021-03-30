namespace ShadrackPhoneRepair.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditRequestModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Requests", "DeviceDescriptionId", "dbo.DeviceDescriptions");
            DropIndex("dbo.Requests", new[] { "BrandId" });
            DropIndex("dbo.Requests", new[] { "DeviceDescriptionId" });
            AddColumn("dbo.Requests", "BrandName", c => c.String());
            AddColumn("dbo.Requests", "DeviceDescriptionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "DeviceDescriptionId");
            AddForeignKey("dbo.Requests", "DeviceDescriptionId", "dbo.DeviceDescriptions", "DeviceDescriptionId", cascadeDelete: true);
            DropColumn("dbo.Requests", "BrandId");
            DropColumn("dbo.Requests", "DeviceDescriptionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "DeviceDescriptionId", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "BrandId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Requests", "DeviceDescriptionId", "dbo.DeviceDescriptions");
            DropIndex("dbo.Requests", new[] { "DeviceDescriptionId" });
            DropColumn("dbo.Requests", "DeviceDescriptionId");
            DropColumn("dbo.Requests", "BrandName");
            CreateIndex("dbo.Requests", "DeviceDescriptionId");
            CreateIndex("dbo.Requests", "BrandId");
            AddForeignKey("dbo.Requests", "DeviceDescriptionId", "dbo.DeviceDescriptions", "DeviceDescriptionId", cascadeDelete: true);
            AddForeignKey("dbo.Requests", "BrandId", "dbo.Brands", "BrandId", cascadeDelete: true);
        }
    }
}
