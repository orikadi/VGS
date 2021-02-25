using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VGS.Models;
using VGS.ViewModels;

namespace VGS.Controllers
{
    public class AdminController : Controller
    {
        private VGSDbContext db = new VGSDbContext();

        //Admin index redirects to Charts
        public ActionResult Index()
        {
            return RedirectToAction("Charts");
        }

        //A page which shows a count for each genre for every studio
        public ActionResult StudioGenreCount()
        {
            if (IsUserAdmin()) //Only admin can view the page
            {
                var query =
                        (from game in db.Games
                         join studio in db.Studios on game.StudioId equals studio.StudioId
                         select new
                         {
                             game,
                             studio
                         } into t1 //join studios and games into {Game, Studio}
                         group t1 by new { t1.studio.StudioName, t1.game.Genre } into g
                         select new
                         {
                             g.Key.StudioName,
                             g.Key.Genre,
                             Count = g.Count()
                         }).OrderBy(x => x.StudioName); //group into {StudioName, Genre, and game count} then order by Studioname
                List<StudioGenreCountViewModel> list = new List<StudioGenreCountViewModel>(); //Create a list of relevant viewmodel
                foreach (var item in query)
                {
                    list.Add(new StudioGenreCountViewModel(item.StudioName, item.Genre, item.Count)); //add the vars into the viewmodel list
                }
                return View(list); //return the view
            }
            return View("Error"); //return error page
        }


        //a page which shows two income charts (studios and genres)
        public ActionResult Charts()
        {
            if (IsUserAdmin()) //only admin can view the page
            {
                var query = from userGame in db.UserGames
                            join game in db.Games on userGame.GameId equals game.GameId //join games and userGames so we will know the amount users who own each game
                            select new
                            {
                                game.Genre,
                                game.Price
                            } into genreCount //select new {Genre, Price}
                            group genreCount by new { genreCount.Genre } into g
                            select new
                            {
                                g.Key.Genre,
                                Count = g.Sum(c => c.Price)
                            }; //Group into genre and total price
                ChartsViewModel charts = new ChartsViewModel(); //create a viewmodel for charts
                foreach (var item in query)
                {
                    charts.GenreTotalPrice.Add(new GenreCount { Genre = item.Genre, TotalPrice = item.Count }); //add the vars into the viewmodel
                }


                var query2 = from userGame in db.UserGames
                             join game in db.Games on userGame.GameId equals game.GameId //join games to userGames
                             join studio in db.Studios on game.StudioId equals studio.StudioId //join studios to games
                             select new
                             {
                                 studio.StudioName,
                                 game.Price
                             } into studioProfit //select new {StudionName, Price}
                             group studioProfit by new { studioProfit.StudioName } into g 
                             select new
                             {
                                 g.Key.StudioName,
                                 Sum = g.Sum(c => c.Price)
                             }; //group into Studioname and total price
                foreach (var item in query2)
                {
                    charts.StudiosProfit.Add(new StudioProfit { StudioName = item.StudioName, TotalProfit = item.Sum }); //add the var into the viewmodel
                }

                return View(charts); //return view
            }
            return View("Error"); //return error page
        }

        //Is the user logged in
        private bool IsUserLogged()
        {
            if (Session["Logged"] == null)
                return false;
            return (bool)Session["Logged"];
        }

        //Is the user an admin
        private bool IsUserAdmin()
        {
            if (IsUserLogged())
                return (int)Session["UserType"] == 1;
            return false;
        }
    }
}