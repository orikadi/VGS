using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VGS.Models;

namespace VGS.Controllers
{
    public class UsersController : Controller
    {
        private VGSDbContext db = new VGSDbContext();

        // GET: Users
        public ActionResult Index()
        {
            if (IsUserLogged()) //only logged users can view user list
                return View(db.Users.ToList());
            else
                return View("Error");
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (IsUserLogged())
            {
                if (UserExists(id.GetValueOrDefault(0)))
                {
                    User user = db.Users.Find(id);
                    return View(user);
                }
                return HttpNotFound();
            }
            return View("Error");
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,Age,Email,Balance,Password,userType")] User user)
        {
            user.UserType = 0;
            user.Balance = 0;
            foreach (User user2 in db.Users)
            {
                bool flag = false;
                if (user2.UserName.Equals(user.UserName))
                {
                    ViewBag.UserNameRegistrationError = "Username is already taken";
                    flag = true;
                }
                if (user2.Email.Equals(user.Email))
                {
                    ViewBag.EmailRegistrationError = "Email is in use already";
                    flag = true;
                }
                if (flag)
                    return View(user); //if email or username is taken, return view with error
            }
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("SignIn");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (UserExists(id.GetValueOrDefault(0)))
            {
                if (IsAllowedToEdit(id.GetValueOrDefault(0)))
                {
                    User user = db.Users.Find(id);
                    return View(user);
                }
                return View("Error");
            }
            return HttpNotFound();
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Age,Email,Balance,Password,userType")] User user)
        {
            bool flag = false;
            foreach (User item in db.Users)
            {
                if (item.UserId != user.UserId && item.Email.Equals(user.Email))
                {
                    flag = true;
                    ViewBag.EmailError = "Email is taken";
                }
            }
            if (!flag)
            {
                db.Entry(db.Users.Where(x => x.UserId == user.UserId).AsQueryable().FirstOrDefault()).CurrentValues.SetValues(user);
                db.SaveChanges();
                if (IsUserAdmin() && user.UserType == 0) //if user edited themsleves from admin to regular
                {
                    Login(user.UserName, user.Password);
                }
                return RedirectToAction("Index", "Home"); //returns to index if edited successfully
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (UserExists(id.GetValueOrDefault(0)))
            {
                if (IsAllowedToEdit(id.GetValueOrDefault(0)))
                {
                    User user = db.Users.Find(id);
                    return View(user);
                }
                return View("Error");
            }
            return HttpNotFound();
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.UserGames.RemoveRange(db.UserGames.Where(x => x.UserId == user.UserId));
            db.SaveChanges();
            if (id == (int)Session["UserId"])
                return Logout();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult SignIn()
        {
            return View();
        }

        public JsonResult Login(string userName, string password)
        {
            foreach (User user in db.Users)
                if (user.UserName.Equals(userName) && user.Password.Equals(password))
                {
                    Session["Logged"] = true;
                    Session["UserId"] = user.UserId;
                    Session["User"] = userName;
                    Session["UserType"] = user.UserType;
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["Logged"] = false;
            Session["User"] = null;
            Session["UserType"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddFundsPage()
        {
            if ((Session["Logged"].Equals(true)))
            {
                int userId = (int)Session["UserId"];
                User user = db.Users.Find(userId);
                return View(user);
            }
            return RedirectToAction("SignIn");
        }

        public JsonResult AddFunds(double? toAdd)
        {
            int? userId = (int)Session["UserId"];
            User user = db.Users.Find(userId);
            if (toAdd.HasValue)
            {
                if (toAdd.Value >= 0 && toAdd.Value <= 10000)
                {
                    user.Balance += Math.Round(toAdd.Value, 2, MidpointRounding.AwayFromZero);
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchIndex(IEnumerable<Studio> collection)
        {
            if (collection == null)
                return View("Error");
            return View(collection);
        }

        public ActionResult SearchUser(string username, double? age, string email, int? userType)
        {
            bool[] idc = {
                String.IsNullOrEmpty(username),
                !age.HasValue,
                String.IsNullOrEmpty(email),
                !userType.HasValue};

            List<User> users = new List<User>();
            foreach (User user in db.Users)
            {
                if (!idc[0])
                    if (!username.Equals(user.UserName))
                        continue;
                if (!idc[1])
                    if (age.Value != user.Age)
                        continue;
                if (!idc[2])
                    if (!email.Equals(user.Email))
                        continue;
                if (!idc[3])
                    if (userType.Value != user.UserType)
                        continue;
                users.Add(user);
            }
            return View("SearchIndex", users);
        }

        public ActionResult Search()
        {
            if (IsUserLogged())
                return View(db.Users.ToList());
            else
                return View("Error");
        }

        public ActionResult ViewGames(int id)
        {
            if (IsAllowedToEdit(id))
            {
                List<Game> games = db.UserGames.Where(x => x.UserId == id).Include(x => x.Game).Select(x => x.Game).Include(x => x.Studio).OrderBy(x => x.Studio.StudioId).ToList();
                TempData["ViewUserGames"] = games;
                return RedirectToAction("SearchIndex", "Games");
            }
            return View("Error");
        }

        private bool IsUserLogged()
        {
            if (Session["Logged"] == null)
                return false;
            return (bool)Session["Logged"];
        }

        private bool IsUserAdmin()
        {
            if (IsUserLogged())
                return (int)Session["UserType"] == 1;
            return false;
        }

        private bool IsAllowedToEdit(int id)
        {
            if (IsUserLogged())
            {
                if (IsUserAdmin())
                    return true;
                else
                    return (int)Session["UserId"] == id;
            }
            return false;
        }
        private bool UserExists(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
                return false;
            return true;
        }
    }
}
