namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeoffestChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClassDetails", "Created", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.ClassDetails", "Updated", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Users", "Created", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Users", "Updated", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.FeeDetails", "Created", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.FeeDetails", "Updated", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Students", "Created", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Students", "Updated", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Subjects", "Created", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Subjects", "Updated", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Teachers", "DateofBirth", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Teachers", "Created", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Teachers", "Updated", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.TeacherSubjectDetails", "Created", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.TeacherSubjectDetails", "Updated", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TeacherSubjectDetails", "Updated", c => c.DateTime());
            AlterColumn("dbo.TeacherSubjectDetails", "Created", c => c.DateTime());
            AlterColumn("dbo.Teachers", "Updated", c => c.DateTime());
            AlterColumn("dbo.Teachers", "Created", c => c.DateTime());
            AlterColumn("dbo.Teachers", "DateofBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Subjects", "Updated", c => c.DateTime());
            AlterColumn("dbo.Subjects", "Created", c => c.DateTime());
            AlterColumn("dbo.Students", "Updated", c => c.DateTime());
            AlterColumn("dbo.Students", "Created", c => c.DateTime());
            AlterColumn("dbo.FeeDetails", "Updated", c => c.DateTime());
            AlterColumn("dbo.FeeDetails", "Created", c => c.DateTime());
            AlterColumn("dbo.Users", "Updated", c => c.DateTime());
            AlterColumn("dbo.Users", "Created", c => c.DateTime());
            AlterColumn("dbo.ClassDetails", "Updated", c => c.DateTime());
            AlterColumn("dbo.ClassDetails", "Created", c => c.DateTime());
        }
    }
}
