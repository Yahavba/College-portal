using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkProject.Models
{
    public class CoursesAndStudents
    {
        public List<User> users { get; set; }
        public List<Course> Courses { get; set; }
    }
}