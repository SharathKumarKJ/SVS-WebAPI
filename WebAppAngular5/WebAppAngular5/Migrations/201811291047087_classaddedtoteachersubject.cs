namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classaddedtoteachersubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherSubjectDetails", "ClassDetail_Id", c => c.Long());
            CreateIndex("dbo.TeacherSubjectDetails", "ClassDetail_Id");
            AddForeignKey("dbo.TeacherSubjectDetails", "ClassDetail_Id", "dbo.ClassDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherSubjectDetails", "ClassDetail_Id", "dbo.ClassDetails");
            DropIndex("dbo.TeacherSubjectDetails", new[] { "ClassDetail_Id" });
            DropColumn("dbo.TeacherSubjectDetails", "ClassDetail_Id");
        }
    }
}
