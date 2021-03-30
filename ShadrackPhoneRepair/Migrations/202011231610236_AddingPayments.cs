namespace ShadrackPhoneRepair.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPayments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentsId = c.Int(nullable: false, identity: true),
                        Cash = c.Boolean(nullable: false),
                        Card = c.Boolean(nullable: false),
                        CardNumber = c.String(maxLength: 16),
                        CVVnumber = c.String(maxLength: 3),
                        ExpiryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentsId);
            
            CreateTable(
                "dbo.PaymentStatus",
                c => new
                    {
                        PaymentStatusId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.PaymentStatusId);
            
            AddColumn("dbo.Requests", "PaymentStatusId", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "PaymentsId", c => c.Int(nullable: false));
            AddColumn("dbo.WalkInRequests", "WalkInTimesId", c => c.Int(nullable: false));
            AddColumn("dbo.WalkInRequests", "PaymentStatusId", c => c.Int(nullable: false));
            AddColumn("dbo.WalkInRequests", "PaymentsId", c => c.Int(nullable: false));
            AlterColumn("dbo.DeviceStatusWalkIns", "WalkInTime", c => c.String());
            AlterColumn("dbo.Requests", "IMEI", c => c.String(maxLength: 15));
            AlterColumn("dbo.WalkInRequests", "IMEI", c => c.String(maxLength: 15));
            CreateIndex("dbo.Requests", "PaymentStatusId");
            CreateIndex("dbo.Requests", "PaymentsId");
            CreateIndex("dbo.WalkInRequests", "WalkInTimesId");
            CreateIndex("dbo.WalkInRequests", "PaymentStatusId");
            CreateIndex("dbo.WalkInRequests", "PaymentsId");
            AddForeignKey("dbo.Requests", "PaymentsId", "dbo.Payments", "PaymentsId", cascadeDelete: true);
            AddForeignKey("dbo.Requests", "PaymentStatusId", "dbo.PaymentStatus", "PaymentStatusId", cascadeDelete: true);
            AddForeignKey("dbo.WalkInRequests", "PaymentsId", "dbo.Payments", "PaymentsId", cascadeDelete: true);
            AddForeignKey("dbo.WalkInRequests", "PaymentStatusId", "dbo.PaymentStatus", "PaymentStatusId", cascadeDelete: true);
            AddForeignKey("dbo.WalkInRequests", "WalkInTimesId", "dbo.WalkInTimes", "WalkInTimesId", cascadeDelete: true);
            DropColumn("dbo.WalkInRequests", "WalkInTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WalkInRequests", "WalkInTime", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.WalkInRequests", "WalkInTimesId", "dbo.WalkInTimes");
            DropForeignKey("dbo.WalkInRequests", "PaymentStatusId", "dbo.PaymentStatus");
            DropForeignKey("dbo.WalkInRequests", "PaymentsId", "dbo.Payments");
            DropForeignKey("dbo.Requests", "PaymentStatusId", "dbo.PaymentStatus");
            DropForeignKey("dbo.Requests", "PaymentsId", "dbo.Payments");
            DropIndex("dbo.WalkInRequests", new[] { "PaymentsId" });
            DropIndex("dbo.WalkInRequests", new[] { "PaymentStatusId" });
            DropIndex("dbo.WalkInRequests", new[] { "WalkInTimesId" });
            DropIndex("dbo.Requests", new[] { "PaymentsId" });
            DropIndex("dbo.Requests", new[] { "PaymentStatusId" });
            AlterColumn("dbo.WalkInRequests", "IMEI", c => c.String());
            AlterColumn("dbo.Requests", "IMEI", c => c.String());
            AlterColumn("dbo.DeviceStatusWalkIns", "WalkInTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.WalkInRequests", "PaymentsId");
            DropColumn("dbo.WalkInRequests", "PaymentStatusId");
            DropColumn("dbo.WalkInRequests", "WalkInTimesId");
            DropColumn("dbo.Requests", "PaymentsId");
            DropColumn("dbo.Requests", "PaymentStatusId");
            DropTable("dbo.PaymentStatus");
            DropTable("dbo.Payments");
        }
    }
}
