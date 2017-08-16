namespace FinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseEnrolment")]
    public partial class CourseEnrolment
    {
        [Key]
        public string CourseEnrolmentId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Student")]
        public string StudentId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Course")]
        public string CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; }
    }
}
