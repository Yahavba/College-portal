using NetworkProject.Dal;
using NetworkProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkProject.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Home()
        {
            /* check if user had logged in */
            if (Session["ID"] == null || Session["password"] == null || Session["type"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            /* check if user is student type */
            else if (!Session["type"].Equals("Student"))
            {
                return RedirectToAction("Home", Session["type"].ToString());
            }
            return View();
        }

        public ActionResult Schedule()
        {
            /* check if user had logged in */
            if (Session["ID"] == null || Session["password"] == null || Session["type"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            /* check if user is student type */
            else if (!Session["type"].Equals("Student"))
            {
                return RedirectToAction("Home", Session["type"].ToString());
            }

            /* get student Schedule */
            using (CourseParticipantsDal courseParticipantDb = new CourseParticipantsDal())
            using (CoursesDal coursesDb = new CoursesDal())
            {
                var id = Session["ID"].ToString();

                /* get student course id for all his courses */
                var courseIds =
                    (from row in courseParticipantDb.CourseParticipants
                     where row.ID.Equals(id)
                     select row.courseId).ToList();

                /* get all  student courses sorted by time */
                var courses = 
                    (from row in coursesDb.Courses
                     where courseIds.Contains(row.courseId)
                     orderby row.startTime
                     select row).ToList();

                /* create Schedule model - all courses in each day */
                Schedule studentSchedule = new Schedule();
                studentSchedule.Sunday = courses.Where(x => x.day.Equals("Sunday")).ToList();
                studentSchedule.Monday = courses.Where(x => x.day.Equals("Monday")).ToList();
                studentSchedule.Tuesday = courses.Where(x => x.day.Equals("Tuesday")).ToList();
                studentSchedule.Wednesday = courses.Where(x => x.day.Equals("Wednesday")).ToList();
                studentSchedule.Thursday = courses.Where(x => x.day.Equals("Thursday")).ToList();
                studentSchedule.Friday = courses.Where(x => x.day.Equals("Friday")).ToList();
                studentSchedule.Saturday = courses.Where(x => x.day.Equals("Saturday")).ToList();
                return View(studentSchedule);
            }
        }

        public ActionResult Exams()
        {
            /* check if user had logged in system */
                if (Session["ID"] == null || Session["password"] == null || Session["type"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            /* check if user is student type */
            else if (!Session["type"].Equals("Student"))
            {
                return RedirectToAction("Home", Session["type"].ToString());
            }

            /* get student Exams */
            using (CourseParticipantsDal courseParticipantDb = new CourseParticipantsDal())
            using (ExamsDal examsDb = new ExamsDal())
            {
                var id = Session["ID"].ToString();

                /* get student course id for all his courses */
                var courses =
                    (from row in courseParticipantDb.CourseParticipants
                     where row.ID.Equals(id)
                     select row.courseId).ToList();

                /* get student Exams for all his courses sort by date and time */
                var exams =
                    (from row in examsDb.Exams
                    where courses.Contains(row.courseId)
                     select row).OrderBy(x => x.date).ThenBy(y => y.startTime).ToList();

                /* create Exam list model */
                ExamList studentExams = new ExamList();
                studentExams.exams = new List<Exam>();
                foreach (Exam x in exams)
                {
                    studentExams.exams.Add(x);
                }
                return View(studentExams);
            }

        }

        public ActionResult Grades()
        {
            /* check if user had logged in */
            if (Session["ID"] == null || Session["password"] == null || Session["type"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            /* check if user is student type */
            else if (!Session["type"].Equals("Student"))
            {
                return RedirectToAction("Home", Session["type"].ToString());
            }

            /* get student grades */
            using (GradesDal gradesDb = new GradesDal())
            {
                var id = Session["ID"].ToString();

                /* get student courses he took moed B */
                var Bgrades =
                    (from row in gradesDb.Grades
                     where row.ID.Equals(id) && row.moed == "B"
                     select row).ToList();

                /* get moed B course id */
                var moedBCourses =
                    (from row in gradesDb.Grades
                     where row.ID.Equals(id) && row.moed == "B"
                     select row.courseId).ToList();

                /* get all moad A grades from courses student didnt take moed B */
                var Agrades =
                    (from row in gradesDb.Grades
                     where row.ID.Equals(id) && row.moed == "A" && !moedBCourses.Contains(row.courseId)
                     select row).ToList();

                /* create grade list model */
                GradeList studenGrades = new GradeList();
                studenGrades.grades = new List<Grade>();
                foreach (Grade x in Bgrades)
                {
                    studenGrades.grades.Add(x);
                }
                foreach (Grade x in Agrades)
                {
                    studenGrades.grades.Add(x);
                }
                return View(studenGrades);
            }
        }
    }
}