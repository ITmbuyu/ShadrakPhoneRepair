namespace ShadrackPhoneRepair.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditWalkinRequestModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WalkInRequests", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.WalkInRequests", "DeviceNameId", "dbo.DeviceDescriptions");
            DropIndex("dbo.WalkInRequests", new[] { "BrandId" });
            DropIndex("dbo.WalkInRequests", new[] { "DeviceNameId" });
            RenameColumn(table: "dbo.WalkInRequests", name: "DeviceNameId", newName: "DeviceName_DeviceNameId");
            AddColumn("dbo.WalkInRequests", "BrandName", c => c.String());
            AddColumn("dbo.WalkInRequests", "DeviceDescriptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.WalkInRequests", "DeviceName_DeviceNameId", c => c.Int());
            CreateIndex("dbo.WalkInRequests", "DeviceName_DeviceNameId");
            AddForeignKey("dbo.WalkInRequests", "DeviceName_DeviceNameId", "dbo.DeviceDescriptions", "DeviceNameId");
            DropColumn("dbo.WalkInRequests", "BrandId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WalkInRequests", "BrandId", c => c.Int(nullable: false));
            DropForeignKey("dbo.WalkInRequests", "DeviceName_DeviceNameId", "dbo.DeviceDescriptions");
            DropIndex("dbo.WalkInRequests", new[] { "DeviceName_DeviceNameId" });
            AlterColumn("dbo.WalkInRequests", "DeviceName_DeviceNameId", c => c.Int(nullable: false));
            DropColumn("dbo.WalkInRequests", "DeviceDescriptionId");
            DropColumn("dbo.WalkInRequests", "BrandName");
            RenameColumn(table: "dbo.WalkInRequests", name: "DeviceName_DeviceNameId", newName: "DeviceNameId");
            CreateIndex("dbo.WalkInRequests", "DeviceNameId");
            CreateIndex("dbo.WalkInRequests", "BrandId");
            AddForeignKey("dbo.WalkInRequests", "DeviceNameId", "dbo.DeviceDescriptions", "DeviceNameId", cascadeDelete: true);
            AddForeignKey("dbo.WalkInRequests", "BrandId", "dbo.Brands", "BrandId", cascadeDelete: true);
        }
    }
}
