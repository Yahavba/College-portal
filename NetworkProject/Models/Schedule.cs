using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkProject.Models
{
    public class Schedule
    {
        public List<Course> Sunday { get; set; }
        public List<Course> Monday { get; set; }
        public List<Course> Tuesday { get; set; }
        public List<Course> Wednesday { get; set; }
        public List<Course> Thursday { get; set; }
        public List<Course> Friday { get; set; }
        public List<Course> Saturday { get; set; }
    }
}