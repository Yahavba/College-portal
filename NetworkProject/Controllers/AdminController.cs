using NetworkProject.Dal;
using NetworkProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkProject.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Home()
        {
            return RedirectToAction("AddUser", "Admin");
        }

        public ActionResult AddUser()
        {
            /* check if user had logged in */
            if (Session["ID"] == null || Session["password"] == null || Session["type"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            /* check if user is admin type */
            else if (!Session["type"].Equals("Admin"))
            {
                return RedirectToAction("Home", Session["type"].ToString());
            }
            return View(new User());
        }

        [HttpPost]
        public ActionResult SubmitUser(User user)
        {
            if (ModelState.IsValid)
            {
                using (UsersDal userDb = new UsersDal())
                {
                    /* check if user exist */
                    var userInDb = (from row in userDb.Users
                                    where row.ID.Equals(user.ID)
                                    select row).FirstOrDefault();

                    /* if user exist */
                    if (userInDb != null)
                    {
                        /* check no attemp to change type */
                        if (user.type != userInDb.type)
                        {
                            TempData["msg"] = "Cant change user type";
                            return View("AddUser", user);
                        }
                        /* update user */
                        userInDb.name = user.name;
                        userInDb.lastName = user.lastName;
                        userInDb.password = user.password;
                        userDb.SaveChanges();
                        TempData["goodMsg"] = "Updated user";
                        /* redirect with succsees message */
                        return View("AddUser", new User());

                    }
                    /* add user */
                    else
                    {
                        userDb.Users.Add(user);
                        userDb.SaveChanges();
                        TempData["goodMsg"] = "Inserted user";
                        /* redirect with succsees message */
                        return View("AddUser", new User());
                    }
                }
            }
            else
            {
                return View("AddUser",user);
            }
        }
    }
}