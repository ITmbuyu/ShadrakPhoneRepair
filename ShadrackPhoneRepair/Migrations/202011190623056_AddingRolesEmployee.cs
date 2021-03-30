namespace ShadrackPhoneRepair.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRolesEmployee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmpName = c.String(nullable: false, maxLength: 30),
                        EmpSurname = c.String(nullable: false, maxLength: 30),
                        EmpEmail = c.String(nullable: false, maxLength: 50),
                        EmpPassword = c.String(nullable: false),
                        EmpRate = c.Double(nullable: false),
                        EmpHours = c.Int(nullable: false),
                        EmpWage = c.Double(nullable: false),
                        EmployeeRole = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropTable("dbo.Employees");
        }
    }
}
