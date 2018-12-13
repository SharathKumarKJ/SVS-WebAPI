namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeeDetails_PaidDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FeeDetails", "PaidDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FeeDetails", "PaidDate", c => c.DateTime(nullable: false));
        }
    }
}
