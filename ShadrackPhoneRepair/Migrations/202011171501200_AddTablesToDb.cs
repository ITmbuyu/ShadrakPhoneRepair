namespace ShadrackPhoneRepair.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTablesToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        BrandRate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Colours",
                c => new
                    {
                        ColourId = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ColourId);
            
            CreateTable(
                "dbo.DeviceDescriptions",
                c => new
                    {
                        DeviceDescriptionId = c.Int(nullable: false, identity: true),
                        BrandId = c.Int(nullable: false),
                        DeviceName = c.String(),
                    })
                .PrimaryKey(t => t.DeviceDescriptionId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.DeviceDescriptions",
                c => new
                    {
                        DeviceDescriptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DeviceDescriptionId);
            
            CreateTable(
                "dbo.DeviceProblems",
                c => new
                    {
                        DeviceProblemId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CostOfP = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DeviceProblemId);
            
            CreateTable(
                "dbo.DeviceStatus",
                c => new
                    {
                        TrackingNumber = c.String(nullable: false, maxLength: 128),
                        Brand = c.String(),
                        DeviceProblem = c.String(),
                        DeviceName = c.String(),
                        Capacity = c.String(),
                        Colour = c.String(),
                        IMEI = c.String(),
                        Price = c.Double(nullable: false),
                        StatusId = c.Int(nullable: false),
                        RequestDateTime = c.DateTime(nullable: false),
                        UserId = c.String(),
                        TechnicianId = c.String(),
                        RepairStatus_RepairStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.TrackingNumber)
                .ForeignKey("dbo.RepairStatus", t => t.RepairStatus_RepairStatusId)
                .Index(t => t.RepairStatus_RepairStatusId);
            
            CreateTable(
                "dbo.RepairStatus",
                c => new
                    {
                        RepairStatusId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.RepairStatusId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        BrandId = c.Int(nullable: false),
                        DeviceProblemId = c.Int(nullable: false),
                        DeviceDescriptionId = c.Int(nullable: false),
                        StorageId = c.Int(nullable: false),
                        ColourId = c.Int(nullable: false),
                        IMEI = c.String(),
                        Price = c.Double(nullable: false),
                        RequestDateTime = c.DateTime(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.Colours", t => t.ColourId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceDescriptions", t => t.DeviceDescriptionId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceProblems", t => t.DeviceProblemId, cascadeDelete: true)
                .ForeignKey("dbo.Storages", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.BrandId)
                .Index(t => t.DeviceProblemId)
                .Index(t => t.DeviceDescriptionId)
                .Index(t => t.StorageId)
                .Index(t => t.ColourId);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        StorageId = c.Int(nullable: false, identity: true),
                        StorageCapacity = c.String(),
                    })
                .PrimaryKey(t => t.StorageId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WalkInRequests",
                c => new
                    {
                        WalkInRequestId = c.Int(nullable: false, identity: true),
                        BrandId = c.Int(nullable: false),
                        DeviceProblemId = c.Int(nullable: false),
                        DeviceDescriptionId = c.Int(nullable: false),
                        WalkInDate = c.DateTime(nullable: false),
                        WalkInTime = c.DateTime(nullable: false),
                        StorageId = c.Int(nullable: false),
                        ColourId = c.Int(nullable: false),
                        IMEI = c.String(),
                        Price = c.Double(nullable: false),
                        RequestDateTime = c.DateTime(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.WalkInRequestId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.Colours", t => t.ColourId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceDescriptions", t => t.DeviceDescriptionId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceProblems", t => t.DeviceProblemId, cascadeDelete: true)
                .ForeignKey("dbo.Storages", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.BrandId)
                .Index(t => t.DeviceProblemId)
                .Index(t => t.DeviceDescriptionId)
                .Index(t => t.StorageId)
                .Index(t => t.ColourId);
            
            CreateTable(
                "dbo.WalkInTimes",
                c => new
                    {
                        WalkInTimesId = c.Int(nullable: false, identity: true),
                        WalkInTime = c.String(),
                    })
                .PrimaryKey(t => t.WalkInTimesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WalkInRequests", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.WalkInRequests", "DeviceProblemId", "dbo.DeviceProblems");
            DropForeignKey("dbo.WalkInRequests", "DeviceDescriptionId", "dbo.DeviceDescriptions");
            DropForeignKey("dbo.WalkInRequests", "ColourId", "dbo.Colours");
            DropForeignKey("dbo.WalkInRequests", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Requests", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.Requests", "DeviceProblemId", "dbo.DeviceProblems");
            DropForeignKey("dbo.Requests", "DeviceDescriptionId", "dbo.DeviceDescriptions");
            DropForeignKey("dbo.Requests", "ColourId", "dbo.Colours");
            DropForeignKey("dbo.Requests", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.DeviceStatus", "RepairStatus_RepairStatusId", "dbo.RepairStatus");
            DropForeignKey("dbo.DeviceDescriptions", "BrandId", "dbo.Brands");
            DropIndex("dbo.WalkInRequests", new[] { "ColourId" });
            DropIndex("dbo.WalkInRequests", new[] { "StorageId" });
            DropIndex("dbo.WalkInRequests", new[] { "DeviceDescriptionId" });
            DropIndex("dbo.WalkInRequests", new[] { "DeviceProblemId" });
            DropIndex("dbo.WalkInRequests", new[] { "BrandId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Requests", new[] { "ColourId" });
            DropIndex("dbo.Requests", new[] { "StorageId" });
            DropIndex("dbo.Requests", new[] { "DeviceDescriptionId" });
            DropIndex("dbo.Requests", new[] { "DeviceProblemId" });
            DropIndex("dbo.Requests", new[] { "BrandId" });
            DropIndex("dbo.DeviceStatus", new[] { "RepairStatus_RepairStatusId" });
            DropIndex("dbo.DeviceDescriptions", new[] { "BrandId" });
            DropTable("dbo.WalkInTimes");
            DropTable("dbo.WalkInRequests");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Storages");
            DropTable("dbo.Requests");
            DropTable("dbo.RepairStatus");
            DropTable("dbo.DeviceStatus");
            DropTable("dbo.DeviceProblems");
            DropTable("dbo.DeviceDescriptions");
            DropTable("dbo.DeviceDescriptions");
            DropTable("dbo.Colours");
            DropTable("dbo.Brands");
        }
    }
}
