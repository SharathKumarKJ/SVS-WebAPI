namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userroledelete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserRoles", "Created");
            DropColumn("dbo.UserRoles", "Updated");
            DropColumn("dbo.UserRoles", "IsActive");
            DropColumn("dbo.UserRoles", "Discriminator");
            DropColumn("dbo.Roles", "Created");
            DropColumn("dbo.Roles", "Updated");
            DropColumn("dbo.Roles", "IsActive");
            DropColumn("dbo.Roles", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Roles", "IsActive", c => c.Boolean());
            AddColumn("dbo.Roles", "Updated", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.Roles", "Created", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.UserRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserRoles", "IsActive", c => c.Boolean());
            AddColumn("dbo.UserRoles", "Updated", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.UserRoles", "Created", c => c.DateTimeOffset(precision: 7));
        }
    }
}
