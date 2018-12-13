namespace WebAppAngular5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classDetailIDadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "ClassDetail_Id", "dbo.ClassDetails");
            DropIndex("dbo.Students", new[] { "ClassDetail_Id" });
            RenameColumn(table: "dbo.Students", name: "ClassDetail_Id", newName: "ClassDetailId");
            AlterColumn("dbo.Students", "ClassDetailId", c => c.Long(nullable: false));
            CreateIndex("dbo.Students", "ClassDetailId");
            AddForeignKey("dbo.Students", "ClassDetailId", "dbo.ClassDetails", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "ClassDetailId", "dbo.ClassDetails");
            DropIndex("dbo.Students", new[] { "ClassDetailId" });
            AlterColumn("dbo.Students", "ClassDetailId", c => c.Long());
            RenameColumn(table: "dbo.Students", name: "ClassDetailId", newName: "ClassDetail_Id");
            CreateIndex("dbo.Students", "ClassDetail_Id");
            AddForeignKey("dbo.Students", "ClassDetail_Id", "dbo.ClassDetails", "Id");
        }
    }
}
