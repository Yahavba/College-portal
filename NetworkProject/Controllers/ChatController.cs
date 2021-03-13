using NetworkProject.Dal;
using NetworkProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkProject.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Chat()
        {
            /* check if user had logged in */
            if (Session["ID"] == null || Session["password"] == null || Session["type"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            /* check if user is Faculty */
            else if (Session["type"].Equals("Faculty"))
            {
                /* get all valid user to send msg */
                using (UsersDal userdb = new UsersDal())
                {
                    UserList users = new UserList();
                    users.users = new List<User>();
                    foreach (var x in userdb.Users.Where(x=> x.type.Equals("Lecturer") || x.type.Equals("Student")))
                    {
                        users.users.Add(x);
                    }
                    return View(users);
                }
            }

            /* check if user is Lecturer */
            else if (Session["type"].Equals("Lecturer"))
            {
                /* get all valid user to send msg */
                using (UsersDal userdb = new UsersDal())
                {
                    UserList users = new UserList();
                    users.users = new List<User>();
                    foreach (var x in userdb.Users.Where(x => x.type.Equals("Faculty") || x.type.Equals("Student")))
                    {
                        users.users.Add(x);
                    }
                    return View(users);
                }
            }

            /* user is student */
            else
            {
                /* get all valid user to send msg */
                using (UsersDal userdb = new UsersDal())
                {
                    UserList users = new UserList();
                    users.users = new List<User>();
                    foreach (var x in userdb.Users.Where(x => x.type.Equals("Lecturer") || x.type.Equals("Faculty")))
                    {
                        users.users.Add(x);
                    }
                    return View(users);
                }
            }
        }

        [HttpGet]
        public ActionResult getChats(string senderID, string recieverID)
        {
            /* get all chats between receiver and sender */
            using ( ChatDal chatDb = new ChatDal())
            {
                var msgs = chatDb.messages.Where(x => (x.senderId.Equals(senderID) && x.receiverId.Equals(recieverID)) || (x.senderId.Equals(recieverID) && x.receiverId.Equals(senderID))).OrderBy(y =>
                y.sendTime).ThenBy(z => z.SendDate).ToList();
                return Json(msgs, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
 