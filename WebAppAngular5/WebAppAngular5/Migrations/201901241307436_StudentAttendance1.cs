namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentAttendance1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentAttendances", "ClassDetail_Id", "dbo.ClassDetails");
            DropIndex("dbo.StudentAttendances", new[] { "ClassDetail_Id" });
            DropColumn("dbo.StudentAttendances", "ClassDetail_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentAttendances", "ClassDetail_Id", c => c.Long());
            CreateIndex("dbo.StudentAttendances", "ClassDetail_Id");
            AddForeignKey("dbo.StudentAttendances", "ClassDetail_Id", "dbo.ClassDetails", "Id");
        }
    }
}
