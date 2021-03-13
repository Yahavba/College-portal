using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetworkProject.Models
{
    public class User
    {
        [Key]
        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "ID must be 9 characters")]
        public string ID { get; set; }

        [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "User Name must be between 2 - 50 characters")]
        public string name { get; set; }

        [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "User Last Name must be between 2 - 50 characters")]
        public string lastName { get; set; }

        [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "User password must be between 2 - 50 characters")]
        public string password { get; set; }

        [Required, RegularExpression("Student|| Lecturer || Faculty", ErrorMessage = "User type must be Student or Lecturer or Faculty")]
        public string type { get; set; }
    }
}