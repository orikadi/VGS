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
    public class UserGamesController : Controller
    {
        private VGSDbContext db = new VGSDbContext();

        // GET: UserGames
        public ActionResult Index()
        {
            if (IsUserOwner())
            {
                var userGames = db.UserGames.Include(u => u.Game).Include(u => u.User);
                return View(userGames.ToList());
            }
            else
            {
                return View("Error");
            }
        }

        // GET: UserGames/Details/5
        public ActionResult Details(int? id)
        {
            if (IsUserOwner())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var userGameResult = db.UserGames.Where(ug => ug.UserGameId == id).Include(u => u.Game).Include(u => u.User);
                UserGame userGame = userGameResult.ToList().First();
                if (userGame == null)
                {
                    return HttpNotFound();
                }
                return View(userGame);
            }
            return View("Error");
        }

        // GET: UserGames/Create
        public ActionResult Create()
        {
            if (IsUserOwner())
            {
                ViewBag.GameId = new SelectList(db.Games, "GameId", "GameName");
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
                return View();
            } else
            {
                return View("Error");
            }
        }

        // POST: UserGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserGameId,UserId,GameId")] UserGame userGame)
        {
            if (IsUserOwner())
            {
                if (ModelState.IsValid)
                {
                    db.UserGames.Add(userGame);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.GameId = new SelectList(db.Games, "GameId", "GameName", userGame.GameId);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", userGame.UserId);
                return View(userGame);
            } else
            {
                return View("Error");
            }
        }

        // GET: UserGames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (IsUserOwner())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserGame userGame = db.UserGames.Find(id);
                if (userGame == null)
                {
                    return HttpNotFound();
                }
                ViewBag.GameId = new SelectList(db.Games, "GameId", "GameName", userGame.GameId);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", userGame.UserId);
                return View(userGame);
            } else
            {
                return View("Error");
            }
        }

        // POST: UserGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserGameId,UserId,GameId")] UserGame userGame)
        {
            if (IsUserOwner())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(userGame).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.GameId = new SelectList(db.Games, "GameId", "GameName", userGame.GameId);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", userGame.UserId);
                return View(userGame);

            }
            else {
                return View("Error");
            }
        }

        // GET: UserGames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (IsUserOwner())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserGame userGame = db.UserGames.Find(id);
                if (userGame == null)
                {
                    return HttpNotFound();
                }
                return View(userGame);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: UserGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsUserOwner())
            {
                UserGame userGame = db.UserGames.Find(id);
                db.UserGames.Remove(userGame);
                db.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                return View("Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IsUserLogged()
        {
            if (Session["Logged"] == null)
                return false;
            return (bool)Session["Logged"];
        }

        private bool IsUserOwner()
        {
            if (IsUserLogged())
                return (int)Session["UserType"] == 2;
            return false;
        }
    }
}
