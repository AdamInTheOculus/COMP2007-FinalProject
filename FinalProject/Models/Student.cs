namespace FinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {

        [Key]
        public string StudentId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Student Name")]
        public string Name { get; set; }

        public double? Average { get; set; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; }


        [Display(Name = "Course Enrolment")]
        [InverseProperty("Course")]
        public virtual ICollection<CourseEnrolment> CourseEnrolments { get; set; } = new HashSet<CourseEnrolment>();

        public override string ToString()
        {
            return String.Format("{0}", Name);
        }
    }
}
