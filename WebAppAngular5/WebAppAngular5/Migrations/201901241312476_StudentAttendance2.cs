namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentAttendance2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentAttendances", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentAttendances", "IsActive");
        }
    }
}
