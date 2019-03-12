namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "Created", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.Roles", "Updated", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.Roles", "IsActive", c => c.Boolean());
            AddColumn("dbo.Roles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "Discriminator");
            DropColumn("dbo.Roles", "IsActive");
            DropColumn("dbo.Roles", "Updated");
            DropColumn("dbo.Roles", "Created");
        }
    }
}
