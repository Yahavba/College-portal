using NetworkProject.Dal;
using NetworkProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkProject.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            /* make sure all user session parameters are empty */
            Session["ID"] = null;
            Session["password"] = null;
            Session["type"] = null;
            Session["name"] = null;
            Session["LastName"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult SubmitLogin(User user)
        {
            /* remove non existing attributes in view form */
            ModelState.Remove("name");
            ModelState.Remove("lastName");
            ModelState.Remove("type");

            if (ModelState.IsValid)
            {
                /* find user in db */
                UsersDal db = new UsersDal();
                User obj = db.Users.Where(User => User.ID.Equals(user.ID) && User.password.Equals(user.password)).FirstOrDefault();


                /* if user exist */
                if (obj != null)
                {
                    Session["ID"] = obj.ID;
                    Session["password"] = obj.password;
                    Session["type"] = obj.type;
                    Session["name"] = obj.name;
                    Session["lastName"] = obj.lastName;
                    return RedirectToAction("HOME", obj.type);
                }

                /* if user doesnt exist */
                else
                {
                    TempData["loginMsg"] = "Incorrect User Name or Password";
                    return RedirectToAction("Login", "Login");
                }
            }

            /* model not valid */
            return RedirectToAction("Login", "Login", user);

        }
    }
}