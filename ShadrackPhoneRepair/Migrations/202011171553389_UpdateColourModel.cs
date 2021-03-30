namespace ShadrackPhoneRepair.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColourModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Colours", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Colours", "Name", c => c.Int(nullable: false));
        }
    }
}
