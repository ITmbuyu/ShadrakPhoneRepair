namespace ShadrackPhoneRepair.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingPaymentsFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "PaymentsId", "dbo.Payments");
            DropForeignKey("dbo.WalkInRequests", "PaymentsId", "dbo.Payments");
            DropIndex("dbo.Requests", new[] { "PaymentsId" });
            DropIndex("dbo.WalkInRequests", new[] { "PaymentsId" });
            DropColumn("dbo.Requests", "PaymentsId");
            DropColumn("dbo.WalkInRequests", "PaymentsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WalkInRequests", "PaymentsId", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "PaymentsId", c => c.Int(nullable: false));
            CreateIndex("dbo.WalkInRequests", "PaymentsId");
            CreateIndex("dbo.Requests", "PaymentsId");
            AddForeignKey("dbo.WalkInRequests", "PaymentsId", "dbo.Payments", "PaymentsId", cascadeDelete: true);
            AddForeignKey("dbo.Requests", "PaymentsId", "dbo.Payments", "PaymentsId", cascadeDelete: true);
        }
    }
}
