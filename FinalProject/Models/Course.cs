namespace FinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        [Key]
        public string CourseId { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Available Semesters")]
        [StringLength(3)]
        public string Available { get; set; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; }

        [Display(Name = "Course Enrolment")]
        [InverseProperty("Student")]
        public virtual ICollection<CourseEnrolment> CourseEnrolments { get; set; } = new HashSet<CourseEnrolment>();
    }
}
