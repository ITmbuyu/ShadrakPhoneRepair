namespace ShadrackPhoneRepair.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentsStatusToStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceStatus", "PaymentStatus", c => c.String());
            AddColumn("dbo.DeviceStatusWalkIns", "PaymentStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeviceStatusWalkIns", "PaymentStatus");
            DropColumn("dbo.DeviceStatus", "PaymentStatus");
        }
    }
}
