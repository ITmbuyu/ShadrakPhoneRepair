namespace ShadrackPhoneRepair.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePayments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequestPayments", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.WalkInPayments", "WalkInRequestId", "dbo.WalkInRequests");
            DropIndex("dbo.RequestPayments", new[] { "RequestId" });
            DropIndex("dbo.WalkInPayments", new[] { "WalkInRequestId" });
            AddColumn("dbo.RequestPayments", "DateTimeofpayment", c => c.DateTime(nullable: false));
            AddColumn("dbo.WalkInPayments", "DateTimeofpayment", c => c.DateTime(nullable: false));
            DropColumn("dbo.RequestPayments", "RequestDateTime");
            DropColumn("dbo.RequestPayments", "RequestId");
            DropColumn("dbo.WalkInPayments", "RequestDateTime");
            DropColumn("dbo.WalkInPayments", "WalkInRequestId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WalkInPayments", "WalkInRequestId", c => c.Int(nullable: false));
            AddColumn("dbo.WalkInPayments", "RequestDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RequestPayments", "RequestId", c => c.Int(nullable: false));
            AddColumn("dbo.RequestPayments", "RequestDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.WalkInPayments", "DateTimeofpayment");
            DropColumn("dbo.RequestPayments", "DateTimeofpayment");
            CreateIndex("dbo.WalkInPayments", "WalkInRequestId");
            CreateIndex("dbo.RequestPayments", "RequestId");
            AddForeignKey("dbo.WalkInPayments", "WalkInRequestId", "dbo.WalkInRequests", "WalkInRequestId", cascadeDelete: true);
            AddForeignKey("dbo.RequestPayments", "RequestId", "dbo.Requests", "RequestId", cascadeDelete: true);
        }
    }
}
