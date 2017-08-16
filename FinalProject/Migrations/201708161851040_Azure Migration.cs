namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AzureMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseEnrolment",
                c => new
                    {
                        CourseEnrolmentId = c.String(nullable: false, maxLength: 128),
                        StudentId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.String(nullable: false, maxLength: 128),
                        CreateDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CourseEnrolmentId)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .ForeignKey("dbo.Student", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 100),
                        Available = c.String(nullable: false, maxLength: 3),
                        CreateDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 100),
                        Average = c.Double(),
                        CreateDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseEnrolment", "StudentId", "dbo.Student");
            DropForeignKey("dbo.CourseEnrolment", "CourseId", "dbo.Course");
            DropIndex("dbo.CourseEnrolment", new[] { "CourseId" });
            DropIndex("dbo.CourseEnrolment", new[] { "StudentId" });
            DropTable("dbo.Student");
            DropTable("dbo.Course");
            DropTable("dbo.CourseEnrolment");
        }
    }
}
