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
    public class StudiosController : Controller
    {
        private VGSDbContext db = new VGSDbContext();

        // GET: Studios
        public ActionResult Index()
        {
            return View(db.Studios.ToList());
        }

        // GET: Studios/Details/5
        public ActionResult Details(int? id)
        {
            if (StudioExists(id.GetValueOrDefault(0)))
            {
                Studio studio = db.Studios.Find(id);
                return View(studio);
            }
            return HttpNotFound();
        }

        // GET: Studios/Create
        public ActionResult Create()
        {

            if (IsUserAdmin())
            {
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        // POST: Studios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudioId,StudioName,Address")] Studio studio)
        {
            if (IsUserAdmin())
            {
                if (db.Studios.AsEnumerable().Where(c => c.StudioName.Equals(studio.StudioName)).Count() > 0)
                {
                    ViewBag.NameError = "Studio already exists";
                    return View(studio);
                }

                if (ModelState.IsValid)
                {
                    db.Studios.Add(studio);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(studio);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Studios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (IsUserAdmin())
            {
                if (StudioExists(id.GetValueOrDefault(0)))
                {
                    if (IsAllowedToEdit())
                    {
                        Studio studio = db.Studios.Find(id);
                        return View(studio);
                    }
                    return View("Error");
                }
                return HttpNotFound();
            }
            else
            {
                return View("Error");
            }
        }

        // POST: Studios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudioId,StudioName,Address")] Studio studio)
        {
            if (IsUserAdmin())
            {
                if (ModelState.IsValid)
                {
                    if (db.Studios.AsEnumerable().Where(c => c.StudioName.Equals(studio.StudioName)&&c.StudioId!=studio.StudioId).Count() > 0) //search same studioName and different id
                    {
                        ViewBag.NameError = "Studio name already exists";
                        return View(studio);
                    }
                    db.Entry(db.Studios.Where(x => x.StudioId == studio.StudioId).AsQueryable().FirstOrDefault()).CurrentValues.SetValues(studio);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(studio);

            }
            else
            {
                return View("Error");
            }
        }

        // GET: Studios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (IsUserAdmin())
            {
                if (StudioExists(id.GetValueOrDefault(0)))
                {
                    if (IsAllowedToEdit())
                    {
                        Studio studio = db.Studios.Find(id);
                        return View(studio);
                    }
                    return View("Error");
                }
                return HttpNotFound();
            }
            else
            {
                return View("Error");
            }
        }

        // POST: Studios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsUserAdmin())
            {
                Studio studio = db.Studios.Find(id);
                List<Game> games = db.Games.Where(x => x.StudioId == studio.StudioId).ToList();
                foreach (var game in games)
                {
                    db.UserGames.RemoveRange(db.UserGames.Where(x => x.GameId == game.GameId));
                }
                db.Studios.Remove(studio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
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

        public ActionResult SearchStudio(string name, string address)
        {
            bool[] idc = { String.IsNullOrEmpty(name),
                           String.IsNullOrEmpty(address)};

            List<Studio> studios = new List<Studio>();
            foreach (Studio studio in db.Studios)
            {
                if (!idc[0])
                    if (!name.ToLower().Split(' ').Any(s => studio.StudioName.ToLower().Contains(s)))//!name.Equals(studio.StudioName)
                        continue;
                if (!idc[1])
                    if (!address.ToLower().Split(' ').Any(s => studio.Address.ToLower().Contains(s)))//!address.Equals(studio.Address))
                        continue;
                studios.Add(studio);
            }
            return View("SearchIndex", studios);
        }

        public ActionResult Search()
        {
            return View();
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

        //only admin is allowed to edit studios
        private bool IsAllowedToEdit()
        {
            if (IsUserLogged())
                return IsUserAdmin();
            return false;
        }

        private bool StudioExists(int id)
        {
            Studio studio = db.Studios.Find(id);
            if (studio == null)
                return false;
            return true;
        }
    }
}
