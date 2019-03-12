namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userrole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRoles", "Created", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.UserRoles", "Updated", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.UserRoles", "IsActive", c => c.Boolean());
            AddColumn("dbo.UserRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRoles", "Discriminator");
            DropColumn("dbo.UserRoles", "IsActive");
            DropColumn("dbo.UserRoles", "Updated");
            DropColumn("dbo.UserRoles", "Created");
        }
    }
}
