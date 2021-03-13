using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkProject.Models
{
    public class CoursesStudentsGrades
    {
        public List<User> users { get; set; }
        public List<Course> courses { get; set; }
        public List<Grade> grades { get; set; }
    }
}