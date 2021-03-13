using NetworkProject.Dal;
using NetworkProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkProject.Controllers
{
    public class LecturerController : Controller
    {
        public ActionResult Home()
        {
            /* check if user had logged in */
            if (Session["ID"] == null || Session["password"] == null || Session["type"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            /* check if user is lecturer type */
            else if (!Session["type"].Equals("Lecturer"))
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
            /* check if user is lecturer type */
            else if (!Session["type"].Equals("Lecturer"))
            {
                return RedirectToAction("Home", Session["type"].ToString());
            }

            /* get lecturer Schedule */
            using (CoursesDal coursesDb = new CoursesDal())
            {
                var id = Session["ID"].ToString();

                /* get all  lecturer courses sorted by time */
                var courses =
                    (from row in coursesDb.Courses
                     where row.lecturer.Equals(id)
                     orderby row.startTime
                     select row).ToList();

                /* create Schedule model - all courses in each day */
                Schedule studentExams = new Schedule();
                studentExams.Sunday = courses.Where(x => x.day.Equals("Sunday")).ToList();
                studentExams.Monday = courses.Where(x => x.day.Equals("Monday")).ToList();
                studentExams.Tuesday = courses.Where(x => x.day.Equals("Tuesday")).ToList();
                studentExams.Wednesday = courses.Where(x => x.day.Equals("Wednesday")).ToList();
                studentExams.Thursday = courses.Where(x => x.day.Equals("Thursday")).ToList();
                studentExams.Friday = courses.Where(x => x.day.Equals("Friday")).ToList();
                studentExams.Saturday = courses.Where(x => x.day.Equals("Saturday")).ToList();
                return View(studentExams);
            }
        }

        public ActionResult MyStudents(string courseIDInput = "")
        {
            /* check if user had logged in */
            if (Session["ID"] == null || Session["password"] == null || Session["type"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            /* check if user is lecturer type */
            else if (!Session["type"].Equals("Lecturer"))
            {
                return RedirectToAction("Home", Session["type"].ToString());
            }

            /* get all lecturer courses and studnt list per course */
            using (CoursesDal coursesDb = new CoursesDal())
            using (UsersDal usersDb = new UsersDal())
            using (CourseParticipantsDal courseParticipantDb = new CourseParticipantsDal())
            {

                var id = Session["ID"].ToString();
                var courseId = coursesDb.Courses.Where(Course => Course.courseId.Equals(courseIDInput) && Course.lecturer.Equals(id)).FirstOrDefault();

                /* get all lecturer courses */
                var courses =
                        (from row in coursesDb.Courses
                         where row.lecturer.Equals(id)
                         select row).ToList();

                /* creae students and courses model */
                CoursesAndStudents obj = new CoursesAndStudents();

                /* add courses to modal */
                obj.Courses = new List<Course>();
                foreach (Course x in courses)
                {
                    obj.Courses.Add(x);
                }

                /* if course isnt empty */
                if (courseId != null)
                {
                    /* get all students id that are in the course */
                    var students =
                        (from row in courseParticipantDb.CourseParticipants
                         where row.courseId.Equals(courseId.courseId)
                         select row.ID).ToList();

                    /* get all students name */
                    var studentNames =
                        (from row in usersDb.Users
                         where students.Contains(row.ID)
                         select row).ToList();

                    /* create student list */
                    obj.users = new List<User>();
                    foreach (User x in studentNames)
                    {
                        obj.users.Add(x);
                    }

                    return View(obj);
                }

                /* course id doesnt exist and course is empty */
                else if (courseIDInput != "")
                {
                    /* create empty list of stuendts and error message */
                    TempData["msg"] = "Invalid course ID";
                    obj.users = new List<User>();
                    return View(obj);
                }

                /* course id doesnt exist and course is empty */
                else
                {
                    /* create empty list of stuendts */
                    obj.users = new List<User>();
                    return View(obj);
                }
            }
        }

        [HttpPost]
        public ActionResult SearchStudents()
        {
            /* form action to get student by course Id */
            return RedirectToAction("MyStudents", "Lecturer", new { courseIDInput = Request.Form["courseId"] });
        }

        public ActionResult Grades(string courseIDInput = "")
        {
            /* check if user had logged in */
            if (Session["ID"] == null || Session["password"] == null || Session["type"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            /* check if user is lecturer type */
            else if (!Session["type"].Equals("Lecturer"))
            {
                return RedirectToAction("Home", Session["type"].ToString());
            }


            using (CoursesDal coursesDb = new CoursesDal())
            using (GradesDal gradesDb = new GradesDal())
            {

                var id = Session["ID"].ToString();
                var courseId = coursesDb.Courses.Where(Course => Course.courseId.Equals(courseIDInput) && Course.lecturer.Equals(id)).FirstOrDefault();


                /* get all lecturer courses */
                var courses =
                        (from row in coursesDb.Courses
                         where row.lecturer.Equals(id)
                         select row).ToList();

                /* creae grades and courses model */
                CoursesAndGrades obj = new CoursesAndGrades();

                /* add courses to modal */
                obj.courses = new List<Course>();
                foreach (Course x in courses)
                {
                    obj.courses.Add(x);
                }

                /* if course isnt empty */
                if (courseId != null)
                {
                    /* get all grades in the course */
                    var studentGrades =
                        (from row in gradesDb.Grades
                         where row.courseId.Equals(courseId.courseId)
                         select row).ToList();

                    obj.grades = new List<Grade>();
                    foreach (Grade x in studentGrades)
                    {
                        obj.grades.Add(x);
                    }

                    return View(obj);
                }

                /* course id doesnt exist and course is not empty */
                else if (courseIDInput != "")
                {
                    /* create empty list of grades and error message */
                    TempData["msg"] = "Invalid course ID";
                    obj.grades = new List<Grade>();
                    return View(obj);
                }

                /* course id doesnt exist and course is empty */
                else
                {
                    /* create empty list of stuendts */
                    obj.grades = new List<Grade>();
                    return View(obj);
                }
            }
        }

        [HttpPost]
        public ActionResult SearchGrades()
        {
            return RedirectToAction("Grades", "Lecturer", new { courseIDInput = Request.Form["CourseID"] });
        }

        public ActionResult InsertGrades()
        {
            /* check if user had logged in */
            if (Session["ID"] == null || Session["password"] == null || Session["type"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            /* check if user is lecturer type */
            else if (!Session["type"].Equals("Lecturer"))
            {
                return RedirectToAction("Home", Session["type"].ToString());
            }
            return View(new Grade());
        }

        [HttpPost]
        public ActionResult InsertGrade()
        {
            Grade grade = new Grade();
            grade.courseId = Request["courseId"].ToString();
            grade.courseName = Request["courseName"].ToString();
            grade.moed = Request["moed"].ToString();
            grade.grade = Int32.Parse(Request["grade"]);
            grade.ID = Request["ID"].ToString();

            var id = Session["ID"].ToString();

            using (CoursesDal coursesDb = new CoursesDal())
            using (ExamsDal exmaDb = new ExamsDal())
            using (UsersDal usersDb = new UsersDal())
            using (GradesDal gradesDb = new GradesDal())
            using (CourseParticipantsDal courseParticipantDb = new CourseParticipantsDal())
            {
                /* check if lecturer passes this course */
                var courseId = coursesDb.Courses.Where(Course => Course.courseId.Equals(grade.courseId) && Course.lecturer.Equals(id)).FirstOrDefault();
                if (courseId == null)
                {
                    TempData["msg"] = "Invalid course ID";
                    return View("InsertGrades", grade);
                }

                /* check course name is the same in courses */
                var courseName = coursesDb.Courses.Where(Course => Course.courseId.Equals(grade.courseId) && Course.courseName.Equals(grade.courseName) && Course.lecturer.Equals(id)).FirstOrDefault();
                if (courseName == null)
                {
                    TempData["msg"] = "Invalid course Name";
                    return View("InsertGrades", grade);
                }

                /* check exam date has passed */
                var exam =
                   (from row in exmaDb.Exams
                    where row.courseId.Equals(courseId.courseId) && row.moed.Equals(grade.moed)
                    select row).FirstOrDefault();

                if (exam == null)
                {
                    TempData["msg"] = "No such exam in system";
                    return View("InsertGrades", grade);
                }

                if (DateTime.Compare(exam.date + exam.endTime, DateTime.Now) > 0)
                {
                    TempData["msg"] = "Exam did not occur yet";
                    return View("InsertGrades", grade);
                }

                /* check if student exist */
                var student =
                       (from row in usersDb.Users
                        where row.ID.Equals(grade.ID)
                        select row).FirstOrDefault();
                if (exam == null)
                {
                    TempData["msg"] = "Student doesnt exist";
                    return View("InsertGrades", grade);

                }
                /* check if student takes this course */
                var studentTakesCourse =
                       (from row in courseParticipantDb.CourseParticipants
                        where row.ID.Equals(grade.ID) && row.courseId.Equals(grade.courseId)
                        select row).FirstOrDefault();
                if (studentTakesCourse == null)
                {
                    TempData["msg"] = "Student doesnt take this course";
                    return View("InsertGrades", grade);

                }

                /* check if student has this course if true update else insert */
                var gradeInDb =
                       (from row in gradesDb.Grades
                        where row.courseId.Equals(grade.courseId) && row.moed.Equals(grade.moed) && row.ID.Equals(grade.ID)
                        select row).FirstOrDefault();
                if (gradeInDb != null)
                {
                    gradeInDb.grade = grade.grade;
                    gradesDb.SaveChanges();
                }
                else
                {
                    gradesDb.Grades.Add(grade);
                    gradesDb.SaveChanges();
                }
            }

            TempData["goodMsg"] = "Inserted\\updated grade";
            /* redirect with succsees message */
            return View("InsertGrades", new Grade());
        }

    }
}