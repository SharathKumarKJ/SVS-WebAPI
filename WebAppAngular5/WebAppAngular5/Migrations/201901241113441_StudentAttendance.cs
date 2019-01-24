namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentAttendances",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AttendanceDate = c.DateTime(nullable: false),
                        IsPresent = c.Boolean(nullable: false),
                        Created = c.DateTimeOffset(precision: 7),
                        Updated = c.DateTimeOffset(precision: 7),
                        ClassDetail_Id = c.Long(),
                        CreatedBy_Id = c.String(maxLength: 128),
                        Student_Id = c.Long(),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassDetails", t => t.ClassDetail_Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .ForeignKey("dbo.Users", t => t.UpdatedBy_Id)
                .Index(t => t.ClassDetail_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.UpdatedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentAttendances", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StudentAttendances", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.StudentAttendances", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StudentAttendances", "ClassDetail_Id", "dbo.ClassDetails");
            DropIndex("dbo.StudentAttendances", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.StudentAttendances", new[] { "Student_Id" });
            DropIndex("dbo.StudentAttendances", new[] { "CreatedBy_Id" });
            DropIndex("dbo.StudentAttendances", new[] { "ClassDetail_Id" });
            DropTable("dbo.StudentAttendances");
        }
    }
}
