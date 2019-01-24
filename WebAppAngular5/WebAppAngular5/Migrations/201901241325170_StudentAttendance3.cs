namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentAttendance3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentAttendances", "Student_Id", "dbo.Students");
            DropIndex("dbo.StudentAttendances", new[] { "Student_Id" });
            RenameColumn(table: "dbo.StudentAttendances", name: "Student_Id", newName: "StudentId");
            AlterColumn("dbo.StudentAttendances", "StudentId", c => c.Long(nullable: false));
            CreateIndex("dbo.StudentAttendances", "StudentId");
            AddForeignKey("dbo.StudentAttendances", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentAttendances", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentAttendances", new[] { "StudentId" });
            AlterColumn("dbo.StudentAttendances", "StudentId", c => c.Long());
            RenameColumn(table: "dbo.StudentAttendances", name: "StudentId", newName: "Student_Id");
            CreateIndex("dbo.StudentAttendances", "Student_Id");
            AddForeignKey("dbo.StudentAttendances", "Student_Id", "dbo.Students", "Id");
        }
    }
}
