namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsActiveAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassDetails", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.FeeDetails", "FeeStatus", c => c.Int(nullable: false));
            AddColumn("dbo.FeeDetails", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Subjects", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Teachers", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.TeacherSubjectDetails", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeacherSubjectDetails", "IsActive");
            DropColumn("dbo.Teachers", "IsActive");
            DropColumn("dbo.Subjects", "IsActive");
            DropColumn("dbo.Students", "IsActive");
            DropColumn("dbo.FeeDetails", "IsActive");
            DropColumn("dbo.FeeDetails", "FeeStatus");
            DropColumn("dbo.Users", "IsActive");
            DropColumn("dbo.ClassDetails", "IsActive");
        }
    }
}
