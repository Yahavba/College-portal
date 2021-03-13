using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetworkProject.Models
{
    public class Grade
    {
        [Key, Column(Order= 0)]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Course Id must be between 2 - 50 characters")]
        public string courseId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Course Name must be between 2 - 50 characters")]
        public string courseName { get; set; }


        [Key, Column(Order = 1)]
        [Required]
        [RegularExpression("[AB]", ErrorMessage = "moed can be A or B")]
        public string moed { get; set; }

        [Required]
        [Range(0,100, ErrorMessage = "Grade can be between 0 - 100")]
        public int grade { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Student ID must be between 9 characters")]
        public string ID { get; set; }
    }
}