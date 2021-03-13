using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetworkProject.Models
{
    public class Message
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "ID must be 9 characters")]
        public string senderId { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "ID must be 9 characters")]
        public string receiverId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "message cant be empty")]
        public string message { get; set; }

        [Required]
        public TimeSpan sendTime { get; set; }

        [Required]
        public DateTime SendDate { get; set; }
    }
}