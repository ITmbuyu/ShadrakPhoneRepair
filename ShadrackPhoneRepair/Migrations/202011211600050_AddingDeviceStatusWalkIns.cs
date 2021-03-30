namespace ShadrackPhoneRepair.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDeviceStatusWalkIns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceStatusWalkIns",
                c => new
                    {
                        TrackingNumber = c.String(nullable: false, maxLength: 128),
                        Brand = c.String(),
                        DeviceProblem = c.String(),
                        DeviceName = c.String(),
                        Capacity = c.String(),
                        Colour = c.String(),
                        IMEI = c.String(),
                        WalkInDate = c.DateTime(nullable: false),
                        WalkInTime = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        WalkInStatus = c.String(),
                        RepairStatusId = c.Int(nullable: false),
                        RequestDateTime = c.DateTime(nullable: false),
                        UserId = c.String(),
                        TechnicianId = c.String(),
                    })
                .PrimaryKey(t => t.TrackingNumber)
                .ForeignKey("dbo.RepairStatus", t => t.RepairStatusId, cascadeDelete: true)
                .Index(t => t.RepairStatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceStatusWalkIns", "RepairStatusId", "dbo.RepairStatus");
            DropIndex("dbo.DeviceStatusWalkIns", new[] { "RepairStatusId" });
            DropTable("dbo.DeviceStatusWalkIns");
        }
    }
}
