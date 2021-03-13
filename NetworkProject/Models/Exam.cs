using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NetworkProject.Models
{
    public class Exam
    {
        [Key, Column(Order = 0)]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Course Id must be between 2 - 50 characters")]
        public string courseId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Course Name must be between 2 - 50 characters")]
        public string courseName { get; set; }

        [Key, Column(Order = 1) ]
        [Required]
        [RegularExpression("A|B", ErrorMessage = "moed can be A or B")]
        public string moed { get; set; }

        [Required]
        public DateTime date { get; set; }

        [Required]
        public TimeSpan startTime { get; set; }

        [Required]
        public TimeSpan endTime { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Class Name must be between 2 - 50 characters")]
        public string className { get; set; }

    }
}