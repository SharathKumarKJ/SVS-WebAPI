namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentMarks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentMarksSheets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StudentId = c.Long(nullable: false),
                        SubjectId = c.Long(nullable: false),
                        ClassDetailId = c.Long(nullable: false),
                        ExamType = c.Int(nullable: false),
                        ResultStatus = c.Int(nullable: false),
                        TotalMarks = c.Long(nullable: false),
                        MarksObtained = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Grade = c.Int(nullable: false),
                        Created = c.DateTimeOffset(precision: 7),
                        Updated = c.DateTimeOffset(precision: 7),
                        CreatedBy_Id = c.String(maxLength: 128),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassDetails", t => t.ClassDetailId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UpdatedBy_Id)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId)
                .Index(t => t.ClassDetailId)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentMarksSheets", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StudentMarksSheets", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StudentMarksSheets", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentMarksSheets", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StudentMarksSheets", "ClassDetailId", "dbo.ClassDetails");
            DropIndex("dbo.StudentMarksSheets", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.StudentMarksSheets", new[] { "CreatedBy_Id" });
            DropIndex("dbo.StudentMarksSheets", new[] { "ClassDetailId" });
            DropIndex("dbo.StudentMarksSheets", new[] { "SubjectId" });
            DropIndex("dbo.StudentMarksSheets", new[] { "StudentId" });
            DropTable("dbo.StudentMarksSheets");
        }
    }
}
