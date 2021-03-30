namespace ShadrackPhoneRepair.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingStatus : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DeviceStatus", "RepairStatus_RepairStatusId", "dbo.RepairStatus");
            DropIndex("dbo.DeviceStatus", new[] { "RepairStatus_RepairStatusId" });
            RenameColumn(table: "dbo.DeviceStatus", name: "RepairStatus_RepairStatusId", newName: "RepairStatusId");
            AlterColumn("dbo.DeviceStatus", "RepairStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.DeviceStatus", "RepairStatusId");
            AddForeignKey("dbo.DeviceStatus", "RepairStatusId", "dbo.RepairStatus", "RepairStatusId", cascadeDelete: true);
            DropColumn("dbo.DeviceStatus", "StatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviceStatus", "StatusId", c => c.Int(nullable: false));
            DropForeignKey("dbo.DeviceStatus", "RepairStatusId", "dbo.RepairStatus");
            DropIndex("dbo.DeviceStatus", new[] { "RepairStatusId" });
            AlterColumn("dbo.DeviceStatus", "RepairStatusId", c => c.Int());
            RenameColumn(table: "dbo.DeviceStatus", name: "RepairStatusId", newName: "RepairStatus_RepairStatusId");
            CreateIndex("dbo.DeviceStatus", "RepairStatus_RepairStatusId");
            AddForeignKey("dbo.DeviceStatus", "RepairStatus_RepairStatusId", "dbo.RepairStatus", "RepairStatusId");
        }
    }
}
