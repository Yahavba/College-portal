using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NetworkProject.Models
{
    public class CourseParticipant
    {
        [Key, Column(Order = 0)]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Course Id must be between 2 - 50 characters")]
        public string courseId { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Student Id must be between 9 characters")]
        public string ID { get; set; }
    }
}