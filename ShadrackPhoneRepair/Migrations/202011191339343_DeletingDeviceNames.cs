namespace ShadrackPhoneRepair.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletingDeviceNames : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WalkInRequests", "DeviceName_DeviceNameId", "dbo.DeviceNames");
            DropIndex("dbo.WalkInRequests", new[] { "DeviceName_DeviceNameId" });
            CreateIndex("dbo.WalkInRequests", "DeviceDescriptionId");
            AddForeignKey("dbo.WalkInRequests", "DeviceDescriptionId", "dbo.DeviceDescriptions", "DeviceDescriptionId", cascadeDelete: true);
            DropColumn("dbo.WalkInRequests", "DeviceName_DeviceNameId");
            DropTable("dbo.DeviceNames");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DeviceNames",
                c => new
                    {
                        DeviceNameId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DeviceNameId);
            
            AddColumn("dbo.WalkInRequests", "DeviceName_DeviceNameId", c => c.Int());
            DropForeignKey("dbo.WalkInRequests", "DeviceDescriptionId", "dbo.DeviceDescriptions");
            DropIndex("dbo.WalkInRequests", new[] { "DeviceDescriptionId" });
            CreateIndex("dbo.WalkInRequests", "DeviceName_DeviceNameId");
            AddForeignKey("dbo.WalkInRequests", "DeviceName_DeviceNameId", "dbo.DeviceNames", "DeviceNameId");
        }
    }
}
