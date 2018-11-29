namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaidAmountAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeeDetails", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.FeeDetails", "PaidAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.FeeDetails", "BalanceAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.FeeDetails", "Amount");
            DropColumn("dbo.FeeDetails", "Balance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FeeDetails", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.FeeDetails", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.FeeDetails", "BalanceAmount");
            DropColumn("dbo.FeeDetails", "PaidAmount");
            DropColumn("dbo.FeeDetails", "TotalAmount");
        }
    }
}
