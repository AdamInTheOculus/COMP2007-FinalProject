namespace FinalProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=ContextModel")
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseEnrolment> CourseEnrolments { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(e => e.CourseEnrolments)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.CourseEnrolments)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);
        }
    }
}
