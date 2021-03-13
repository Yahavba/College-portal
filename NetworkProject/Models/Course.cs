using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NetworkProject.Models
{
    public class Course
    {
        [Key, Column(Order=0), ]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Course Id must be between 2 - 50 characters")]
        public string courseId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Course Name must be between 2 - 50 characters")]
        public string courseName { get; set; }

        [Required]
        [RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage = "Start time must be between 00:00 to 23:59")]
        public TimeSpan startTime { get; set; }

        [Required]
        [RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage = "End time must be between 00:00 to 23:59")]
        public TimeSpan endTime { get; set; }

        [Required]
        [RegularExpression("Sunday|Monday|Tuesday|Wednesday|Thursday|Friday|Satuerday" ,ErrorMessage = "day must be Sunday or Monday or Tuesday or Wednesday or Thursday or Friday or Satuerday")]
        public string day { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Class Name must be between 2 - 50 characters")]
        public string className { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        [StringLength(9, MinimumLength = 9 , ErrorMessage = "lecturer ID must be between 9 characters")]
        public string lecturer { get; set; }

    }
}