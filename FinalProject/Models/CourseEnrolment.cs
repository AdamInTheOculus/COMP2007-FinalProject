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
        public string CourseEnrolmentId { get; set; }

        [Required]
        [StringLength(128)]
        public string StudentId { get; set; }

        [Required]
        [StringLength(128)]
        public string CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual Student Student { get; set; }
    }
}
