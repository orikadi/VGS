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
    public class GamesController : Controller
    {
        private VGSDbContext db = new VGSDbContext();

        //returns the view of all games
        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.Studio);
            return View(games.ToList());
        }

        //returns the details of a game
        public ActionResult Details(int? id)
        {
            if (GameExists(id.GetValueOrDefault(0))) //GetValueOrDefault gives 0 if id is null (a game with id=0 does not exist)
            {
                Game game = db.Games
               .Include(i => i.Studio)
               .SingleOrDefault(x => x.GameId == id);
                return View(game);
            }
            return HttpNotFound(); //return error page
        }

        // return the create game page
        public ActionResult Create() 
        {
            if (IsUserAdmin()) //only an admin can create a game
            {
                ViewBag.StudioId = new SelectList(db.Studios, "StudioId", "StudioName");
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameId,GameName,Genre,StudioId,ReleaseDate,Price,ImagePath,Rating")] Game game)
        {
            if (IsUserAdmin()) //only an admin can create a game
            {
                /* if a game with same name and studio already exists, return error
                 * load the game table and include the studio for each game, turns it into an enumerable and then removes all games except ones with same name and studio
                 * if there is one left, that means it already exists and so deny the creation and return an error*/
                if (db.Games.Include(x => x.Studio).AsEnumerable().Where(c => c.GameName.Equals(game.GameName) && c.StudioId == game.StudioId).Count() > 0) 
                {
                    ViewBag.NameError = "This studio already has a game with this name";
                    ViewBag.StudioId = new SelectList(db.Studios, "StudioId", "StudioName", game.StudioId);
                    return View(game);
                }
                if (ModelState.IsValid)
                {
                    db.Games.Add(game);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.StudioId = new SelectList(db.Studios, "StudioId", "StudioName", game.StudioId);
                return View(game);
            }
            else
            {
                return View("Error");
            }
        }

        // retuns the edit view of a game
        public ActionResult Edit(int? id)
        {
            if (GameExists(id.GetValueOrDefault(0)))
            {
                if (IsAllowedToEdit())
                {
                    Game game = db.Games.Find(id);
                    ViewBag.StudioId = new SelectList(db.Studios, "StudioId", "StudioName", game.StudioId);
                    return View(game);
                }
                return View("Error");
            }
            return HttpNotFound();
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,GameName,Genre,StudioId,ReleaseDate,Price,ImagePath,Rating")] Game game)
        {
            //same as create
            if (db.Games.AsEnumerable().Where(c => c.GameId != game.GameId && c.GameName.Equals(game.GameName) && c.StudioId == game.StudioId).Count() > 0)
            {
                ViewBag.StudioId = new SelectList(db.Studios, "StudioId", "StudioName", game.StudioId);
                ViewBag.NameError = "This studio already has a game with this name";
                return View(game);
            }
            if (ModelState.IsValid)
            {
                db.Entry(db.Games.Where(x => x.GameId == game.GameId).AsQueryable().FirstOrDefault()).CurrentValues.SetValues(game); //if the model is valid, get the game from the database, insert the new values and save it
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudioId = new SelectList(db.Studios, "StudioId", "StudioName", game.StudioId);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (GameExists(id.GetValueOrDefault(0)))
            {
                if (IsAllowedToEdit())
                {
                    Game game = db.Games.Find(id);
                    return View(game);
                }
                return View("Error");
            }
            return HttpNotFound();
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.UserGames.RemoveRange(db.UserGames.Where(x => x.GameId == game.GameId));
            db.SaveChanges();
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
        public ActionResult SearchIndex(IEnumerable<Game> collection)
        {
            if (TempData["ViewUserGames"] != null) //TempData will not be null if SearchIndex is called from Users/ViewGames
                collection = (List<Game>)TempData["ViewUserGames"];
            if (collection == null) //if someone tried to enter this method from URL
                return View("Error");
            return View(collection);
        }

        public ActionResult SearchGame(string name, string genre, string studioName, DateTime? releaseDate, double? price, double? rating)
        {
            bool[] idc = { //empty value check
                String.IsNullOrEmpty(name),
                String.IsNullOrEmpty(genre),
                String.IsNullOrEmpty(studioName),
                !releaseDate.HasValue,
                !price.HasValue,
                !rating.HasValue };

            List<Game> games = new List<Game>();
            foreach (Game game in db.Games.Include(p => p.Studio))
            {
                if (!idc[0])
                    if (!name.ToLower().Split(' ').Any(s => game.GameName.ToLower().Contains(s))) //checks if any of the words in the provided string are in the GameName
                        continue;
                if (!idc[1])
                    if (!genre.ToLower().Split(' ').Any(s => game.Genre.ToLower().Contains(s)))
                        continue;
                if (!idc[2])
                    if (!studioName.ToLower().Split(' ').Any(s => game.Studio.StudioName.ToLower().Contains(s)))
                        continue;
                if (!idc[3])
                    if (!releaseDate.Value.Equals(game.ReleaseDate))
                        continue;
                if (!idc[4])
                    if (price.Value != game.Price)
                        continue;
                if (!idc[5])
                    if (rating.Value != game.Rating)
                        continue;
                games.Add(game);
            }
            return View("SearchIndex", games);
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Recommend()
        {
            if (IsUserLogged()) //Can only recommend to logged user
            {
                int session = (int)Session["userId"]; //Session id of user
                List<Game> userGames = (from userGame in db.UserGames.Include(x => x.Game) //find the games which the user owns
                                        where userGame.UserId == session
                                        select userGame.Game).ToList(); 
                List<Game> gamesByPopularity = (((from p in db.UserGames.Include(x => x.Game) //sort the games by amount of users who own them
                                                  group p by p.Game into gd
                                                  select new { Game = gd.Key, Count = gd.Count() }).OrderByDescending(x => x.Count)).Select(x => x.Game)).ToList();
                if (userGames.Count == 0) //if the user has 0 games, recommend the top 5
                    return View(gamesByPopularity.Take(5).ToList());

                List<string> userGenres = (from game in userGames select game.Genre).Distinct().ToList(); //Create a list of genres the user owns a game with that genre

                //from gamesByPopularity we take the top 5 games which the user does not own and are in their liked genres
                List<Game> offeredGames = (from game in gamesByPopularity select game).Where(x => !userGames.Contains(x) && userGenres.Contains(x.Genre)).Take(5).ToList();
                if (offeredGames.Count == 0) //if there are no recommended games, return the top 5 games
                    return View(gamesByPopularity.Take(5).ToList());

                return View(offeredGames);
            }
            return View("Error");
        }

        // 3-successfully bought. 2-already owned. 1-insufficient funds. 0-not signed in
        public JsonResult Buy(int? gameId)
        {
            if (IsUserLogged())
            {
                int userId = (int)Session["UserId"];
                User user = db.Users.Find(userId);
                Game game = db.Games.Find(gameId);
                bool isOwned = false;
                foreach (var item in db.UserGames.Where(x => x.UserId == userId && x.GameId == gameId)) //go over all games where the userId and GameId match, if there is one like that, we set isOwned ot true
                {
                    isOwned = true;
                    break;
                }
                if (!isOwned && user.Balance >= game.Price)
                {
                    //check if this works correctly
                    user.Balance -= game.Price;
                    UserGame ug = new UserGame
                    {
                        UserId = userId, //cant be null since 'logged' is true
                        User = user,
                        GameId = gameId.Value,
                        Game = game
                    };
                    db.UserGames.Add(ug);
                    db.SaveChanges();
                    return Json(3, JsonRequestBehavior.AllowGet);//bought the game
                }
                else if (isOwned) return Json(2, JsonRequestBehavior.AllowGet); //owns the game
                else return Json(1, JsonRequestBehavior.AllowGet);//not enough money
            }
            return Json(0, JsonRequestBehavior.AllowGet);//not logged in
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

        //only admin is allowed to edit games
        private bool IsAllowedToEdit()
        {
            if (IsUserLogged())
                return IsUserAdmin();
            return false;
        }

        private bool GameExists(int id)
        {
            Game game = db.Games.Find(id);
            if (game == null)
                return false;
            return true;
        }
    }
}
