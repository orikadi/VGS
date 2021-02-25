namespace VGS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VGS.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<VGSDbContext>
    {
        const int NUM_OF_GAMES = 100; //need to be aligned with the amount of games when seeding
        const int NUM_OF_USERS = 5; //needs to be aligned with the amount of users when seeding
        const int NUM_OF_USERGAMES=200;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "VGS.Models.VGSDbContext";
        }

        //maybe need to make a map from name of game or studio to id. After inputing them, we need to get them so the game and usergame can work.
        protected override void Seed(VGSDbContext context)
        {
            Trunicate(context);
            AddStudios(context);
            AddGames(context);
            AddUsers(context);
            AddUserGames(context);
        }

        private void Trunicate(VGSDbContext context)
        {
            //Studios
            var studios = from o in context.Studios select o;
            foreach (var studio in studios)
            {
                context.Studios.Remove(studio);
            }
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Studios', RESEED, 0)");

            //Games
            var games = from o in context.Games select o;
            foreach (var game in games)
            {
                context.Games.Remove(game);
            }
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Games', RESEED, 0)");


            //Users
            var users = from o in context.Users select o;
            foreach (var user in users)
            {
                context.Users.Remove(user);
            }
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Users', RESEED, 0)");

            //UserGames
            var userGames = from o in context.UserGames select o;
            foreach (var userGame in userGames)
            {
                context.UserGames.Remove(userGame);
            }
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('UserGames', RESEED, 0)");

            //SaveChanges
            context.SaveChanges();
        }

        public void AddStudios(VGSDbContext context)
        {
            context.Studios.AddOrUpdate(x => x.StudioName, new Models.Studio() { StudioName = "Bethesda Softworks", Address = "1370 Piccard Drive Rockville, MD 20850" });
            context.Studios.AddOrUpdate(x => x.StudioName, new Models.Studio() { StudioName = "Electronic Arts", Address = "209 Redwood Shores Pkwy, Redwood City, CA 94065" });
            context.Studios.AddOrUpdate(x => x.StudioName, new Models.Studio() { StudioName = "Square Enix", Address = "999 N. Pacific Coast Highway, 3rd Floor, El Segundo, CA 90245" });
            context.Studios.AddOrUpdate(x => x.StudioName, new Models.Studio() { StudioName = "Activision", Address = "3100 Ocean Park Blvd, Santa Monica, CA 90405" });
            context.Studios.AddOrUpdate(x => x.StudioName, new Models.Studio() { StudioName = "CAPCOM CO., LTD.", Address = "185 Berry Street,San Francisco, CA 94107" });
            context.Studios.AddOrUpdate(x => x.StudioName, new Models.Studio() { StudioName = "Codemasters", Address = "Stoneythorpe,Southam Warwickshire, CV47 2DL" });
            context.Studios.AddOrUpdate(x => x.StudioName, new Models.Studio() { StudioName = "PlayWay S.A.", Address = "ul. Bluszczanska 76 lok 6 00-712 Warszawa, Polska" });
            context.SaveChanges();
        }

        public void AddGames(VGSDbContext context)
        {
            Random rnd=new Random();
            // Bethesda Softworks
            
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "RAGE 2", Genre = "Action", StudioId = 1, ReleaseDate = DateTime.Parse("2019-05-14"), Price = 75, ImagePath = "rage2", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Bethesda Softworks
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "The Evil Within 2", Genre = "Horror", StudioId = 1, ReleaseDate = DateTime.Parse("2017-10-12"), Price = 73, ImagePath = "theevilwithin2", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Bethesda Softworks
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Prey", Genre = "Horror", StudioId = 1, ReleaseDate = DateTime.Parse("2017-05-04"), Price = 37, ImagePath = "prey", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Bethesda Softworks
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "DOOM", Genre = "Shooter", StudioId = 1, ReleaseDate = DateTime.Parse("2016-05-13"), Price = 25, ImagePath = "doom", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Bethesda Softworks
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Dishonored", Genre = "Action", StudioId = 1, ReleaseDate = DateTime.Parse("2012-10-12"), Price = 13, ImagePath = "dishonored", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Bethesda Softworks
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "BRINK", Genre = "Shooter", StudioId = 1, ReleaseDate = DateTime.Parse("2011-05-11"), Price = 23, ImagePath = "brink", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Bethesda Softworks
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Fallout 3", Genre = "RPG", StudioId = 1, ReleaseDate = DateTime.Parse("2008-10-28"), Price = 13, ImagePath = "fallout3", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Bethesda Softworks
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Spear of Destiny", Genre = "Action", StudioId = 1, ReleaseDate = DateTime.Parse("1993-10-10"), Price = 5, ImagePath = "spearofdestiny", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Bethesda Softworks
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "QUAKE", Genre = "Shooter", StudioId = 1, ReleaseDate = DateTime.Parse("1996-06-22"), Price = 5, ImagePath = "quake", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Bethesda Softworks
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Commander Keen", Genre = "Platformer", StudioId = 1, ReleaseDate = DateTime.Parse("1990-12-14"), Price = 6, ImagePath = "commanderkeen", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Bethesda Softworks
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Arx Fatalis", Genre = "RPG", StudioId = 1, ReleaseDate = DateTime.Parse("2002-11-12"), Price = 6, ImagePath = "arxfatalis", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Bethesda Softworks
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "The Elder Scrolls V: Skyrim Special Edition", Genre = "RPG", StudioId = 1, ReleaseDate = DateTime.Parse("2016-10-28"), Price = 49, ImagePath = "theelderscrollsvskyrimspecialedition", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Bethesda Softworks
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Wolfenstein 3D", Genre = "Shooter", StudioId = 1, ReleaseDate = DateTime.Parse("1994-08-03"), Price = 6, ImagePath = "wolfenstein3d", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "The Sims 3", Genre = "Simulation", StudioId = 2, ReleaseDate = DateTime.Parse("2009-06-02"), Price = 6, ImagePath = "thesims3", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "SPORE", Genre = "Simulation", StudioId = 2, ReleaseDate = DateTime.Parse("2008-12-19"), Price = 6, ImagePath = "spore", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Mirror's Edge", Genre = "Action", StudioId = 2, ReleaseDate = DateTime.Parse("2009-01-13"), Price = 6, ImagePath = "mirrorsedge", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Dead Space 2", Genre = "Horror", StudioId = 2, ReleaseDate = DateTime.Parse("2011-01-28"), Price = 6, ImagePath = "deadspace2", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Mass Effect 2", Genre = "RPG", StudioId = 2, ReleaseDate = DateTime.Parse("2010-01-29"), Price = 22, ImagePath = "masseffect2", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Mass Effect", Genre = "RPG", StudioId = 2, ReleaseDate = DateTime.Parse("2008-05-28"), Price = 22, ImagePath = "masseffect", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Dead Space", Genre = "Horror", StudioId = 2, ReleaseDate = DateTime.Parse("2008-10-20"), Price = 22, ImagePath = "deadspace", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Jade Empire: Special Edition", Genre = "RPG", StudioId = 2, ReleaseDate = DateTime.Parse("2007-02-27"), Price = 16, ImagePath = "jadeempirespecialedition", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Crysis 2 - Maximum Edition", Genre = "Shooter", StudioId = 2, ReleaseDate = DateTime.Parse("2011-03-22"), Price = 32, ImagePath = "crysis2-maximumedition", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Need for Speed Undercover", Genre = "Racing", StudioId = 2, ReleaseDate = DateTime.Parse("2008-11-28"), Price = 11, ImagePath = "needforspeedundercover", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Dragon Age: Origins", Genre = "RPG", StudioId = 2, ReleaseDate = DateTime.Parse("2009-11-06"), Price = 22, ImagePath = "dragonageorigins", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Crysis", Genre = "Shooter", StudioId = 2, ReleaseDate = DateTime.Parse("2007-11-13"), Price = 22, ImagePath = "crysis", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Medal of Honor", Genre = "Shooter", StudioId = 2, ReleaseDate = DateTime.Parse("2010-10-12"), Price = 22, ImagePath = "medalofhonor", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "DeathSpank", Genre = "RPG", StudioId = 2, ReleaseDate = DateTime.Parse("2010-10-26"), Price = 16, ImagePath = "deathspank", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Warp", Genre = "Action", StudioId = 2, ReleaseDate = DateTime.Parse("2012-03-22"), Price = 11, ImagePath = "warp", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Burnout Paradise: The Ultimate Box", Genre = "Racing", StudioId = 2, ReleaseDate = DateTime.Parse("2009-02-03"), Price = 22, ImagePath = "burnoutparadisetheultimatebox", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Electronic Arts 
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Shift 2 Unleashed", Genre = "Racing", StudioId = 2, ReleaseDate = DateTime.Parse("2011-04-01"), Price = 22, ImagePath = "shift2unleashed", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "OCTOPATH TRAVELER", Genre = "RPG", StudioId = 3, ReleaseDate = DateTime.Parse("2019-06-07"), Price = 63, ImagePath = "octopathtraveler", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "BATTALION 1944", Genre = "Shooter", StudioId = 3, ReleaseDate = DateTime.Parse("2019-05-23"), Price = 20, ImagePath = "battalion1944", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "LEFT ALIVE", Genre = "Action", StudioId = 3, ReleaseDate = DateTime.Parse("2019-03-05"), Price = 65, ImagePath = "leftalive", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Just Cause 4", Genre = "Action", StudioId = 3, ReleaseDate = DateTime.Parse("2018-12-04"), Price = 65, ImagePath = "justcause4", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "THE QUIET MAN", Genre = "Action", StudioId = 3, ReleaseDate = DateTime.Parse("2018-11-01"), Price = 16, ImagePath = "thequietman", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Shadow of the Tomb Raider", Genre = "Action", StudioId = 3, ReleaseDate = DateTime.Parse("2018-09-14"), Price = 65, ImagePath = "shadowofthetombraider", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Forgotton Anne", Genre = "Platformer", StudioId = 3, ReleaseDate = DateTime.Parse("2018-05-15"), Price = 11, ImagePath = "forgottonanne", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Octahedron: Transfixed Edition", Genre = "Platformer", StudioId = 3, ReleaseDate = DateTime.Parse("2018-03-20"), Price = 7, ImagePath = "octahedrontransfixededition", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "FINAL FANTASY XV WINDOWS EDITION", Genre = "RPG", StudioId = 3, ReleaseDate = DateTime.Parse("2018-03-06"), Price = 55, ImagePath = "finalfantasyxvwindowsedition", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Fear Effect Sedna", Genre = "Action", StudioId = 3, ReleaseDate = DateTime.Parse("2018-03-06"), Price = 3, ImagePath = "feareffectsedna", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "CHRONO TRIGGER", Genre = "RPG", StudioId = 3, ReleaseDate = DateTime.Parse("2018-02-27"), Price = 16, ImagePath = "chronotrigger", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Secret of Mana", Genre = "RPG", StudioId = 3, ReleaseDate = DateTime.Parse("2018-02-15"), Price = 43, ImagePath = "secretofmana", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "FINAL FANTASY XII THE ZODIAC AGE", Genre = "RPG", StudioId = 3, ReleaseDate = DateTime.Parse("2018-02-01"), Price = 55, ImagePath = "finalfantasyxiithezodiacage", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "LOST SPHEAR", Genre = "RPG", StudioId = 3, ReleaseDate = DateTime.Parse("2018-01-23"), Price = 55, ImagePath = "lostsphear", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Oh My Godheads", Genre = "Action", StudioId = 3, ReleaseDate = DateTime.Parse("2017-12-05"), Price = 4, ImagePath = "ohmygodheads", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Spelunker Party!", Genre = "Action", StudioId = 3, ReleaseDate = DateTime.Parse("2017-10-19"), Price = 34, ImagePath = "spelunkerparty!", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Deadbeat Heroes", Genre = "Action", StudioId = 3, ReleaseDate = DateTime.Parse("2017-10-10"), Price = 8, ImagePath = "deadbeatheroes", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Tokyo Dark", Genre = "Horror", StudioId = 3, ReleaseDate = DateTime.Parse("2017-09-07"), Price = 12, ImagePath = "tokyodark", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Black The Fall", Genre = "Action", StudioId = 3, ReleaseDate = DateTime.Parse("2017-07-11"), Price = 28, ImagePath = "blackthefall", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Murdered: Soul Suspect", Genre = "Horror", StudioId = 3, ReleaseDate = DateTime.Parse("2014-06-06"), Price = 4, ImagePath = "murderedsoulsuspect", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Startopia", Genre = "Simulation", StudioId = 3, ReleaseDate = DateTime.Parse("2001-06-19"), Price = 4, ImagePath = "startopia", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Pandemonium", Genre = "Platformer", StudioId = 3, ReleaseDate = DateTime.Parse("1996-10-31"), Price = 1, ImagePath = "pandemonium", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Conflict Desert Storm", Genre = "Shooter", StudioId = 3, ReleaseDate = DateTime.Parse("2002-09-13"), Price = 2, ImagePath = "conflictdesertstorm", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Scary Girl", Genre = "Platformer", StudioId = 3, ReleaseDate = DateTime.Parse("2012-04-09"), Price = 1, ImagePath = "scarygirl", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Front Mission Evolved", Genre = "Shooter", StudioId = 3, ReleaseDate = DateTime.Parse("2010-10-08"), Price = 11, ImagePath = "frontmissionevolved", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Square Enix
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Battlestations Pacific", Genre = "Action", StudioId = 3, ReleaseDate = DateTime.Parse("2009-05-12"), Price = 1, ImagePath = "battlestationspacific", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Call of Duty: Black Ops III", Genre = "Shooter", StudioId = 4, ReleaseDate = DateTime.Parse("2015-11-06"), Price = 61, ImagePath = "callofdutyblackopsiii", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Call of Duty: Modern Warfare 2", Genre = "Shooter", StudioId = 4, ReleaseDate = DateTime.Parse("2009-11-12"), Price = 33, ImagePath = "callofdutymodernwarfare2", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Prototype", Genre = "Action", StudioId = 4, ReleaseDate = DateTime.Parse("2009-06-10"), Price = 7, ImagePath = "prototype", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Crash Bandicoot N. Sane Trilogy", Genre = "Platformer", StudioId = 4, ReleaseDate = DateTime.Parse("2018-06-29"), Price = 55, ImagePath = "crashbandicootn.sanetrilogy", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Pharaoh + Cleopatra", Genre = "Simulation", StudioId = 4, ReleaseDate = DateTime.Parse("1997-10-31"), Price = 10, ImagePath = "pharaohcleopatra", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Zeus + Poseidon", Genre = "Simulation", StudioId = 4, ReleaseDate = DateTime.Parse("2016-12-15"), Price = 10, ImagePath = "zeusposeidon", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Caesar 3", Genre = "Simulation", StudioId = 4, ReleaseDate = DateTime.Parse("1999-05-30"), Price = 12, ImagePath = "caesar3", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Call of Duty", Genre = "Shooter", StudioId = 4, ReleaseDate = DateTime.Parse("2003-10-29"), Price = 20, ImagePath = "callofduty", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Singularity", Genre = "Shooter", StudioId = 4, ReleaseDate = DateTime.Parse("2010-07-01"), Price = 30, ImagePath = "singularity", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Caesar IV", Genre = "Simulation", StudioId = 4, ReleaseDate = DateTime.Parse("2006-09-09"), Price = 10, ImagePath = "caesariv", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Spyro Reignited Trilogy", Genre = "Platformer", StudioId = 4, ReleaseDate = DateTime.Parse("2019-09-03"), Price = 55, ImagePath = "spyroreignitedtrilogy", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "SWAT 3: Tactical Game of the Year Edition", Genre = "Simulation", StudioId = 4, ReleaseDate = DateTime.Parse("2001-10-10"), Price = 10, ImagePath = "swat3tacticalgameoftheyearedition", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Phantasmagoria", Genre = "Horror", StudioId = 4, ReleaseDate = DateTime.Parse("1995-07-31"), Price = 10, ImagePath = "phantasmagoria", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Phantasmagoria 2: A Puzzle of Flesh", Genre = "Horror", StudioId = 4, ReleaseDate = DateTime.Parse("1996-12-03"), Price = 6, ImagePath = "phantasmagoria2apuzzleofflesh", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Aces of the Galaxy", Genre = "Action", StudioId = 4, ReleaseDate = DateTime.Parse("2008-06-04"), Price = 10, ImagePath = "acesofthegalaxy", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Activision
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Police Quest: SWAT", Genre = "Simulation", StudioId = 4, ReleaseDate = DateTime.Parse("1996-09-30"), Price = 6, ImagePath = "policequestswat", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //  CAPCOM CO., LTD.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "RESIDENT EVIL 2 / BIOHAZARD RE:2", Genre = "Horror", StudioId = 5, ReleaseDate = DateTime.Parse("2019-01-25"), Price = 82, ImagePath = "residentevil2biohazardre2", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //  CAPCOM CO., LTD.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Mega Man 11", Genre = "Platformer", StudioId = 5, ReleaseDate = DateTime.Parse("2018-10-02"), Price = 41, ImagePath = "megaman11", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //  CAPCOM CO., LTD.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "The Disney Afternoon Collection", Genre = "Platformer", StudioId = 5, ReleaseDate = DateTime.Parse("2007-04-18"), Price = 28, ImagePath = "thedisneyafternooncollection", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //  CAPCOM CO., LTD.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "STRIDER", Genre = "Platformer", StudioId = 5, ReleaseDate = DateTime.Parse("2014-02-19"), Price = 10, ImagePath = "strider", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //  CAPCOM CO., LTD.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "DuckTales: Remastered", Genre = "Platformer", StudioId = 5, ReleaseDate = DateTime.Parse("2013-08-13"), Price = 5, ImagePath = "ducktalesremastered", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //  CAPCOM CO., LTD.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Dark Void Zero", Genre = "Platformer", StudioId = 5, ReleaseDate = DateTime.Parse("2010-04-12"), Price = 5, ImagePath = "darkvoidzero", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //  CAPCOM CO., LTD.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Bionic Commando: Rearmed", Genre = "Platformer", StudioId = 5, ReleaseDate = DateTime.Parse("2008-08-14"), Price = 12, ImagePath = "bioniccommandorearmed", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //  CAPCOM CO., LTD.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "DEAD RISING", Genre = "Horror", StudioId = 5, ReleaseDate = DateTime.Parse("2016-09-13"), Price = 28, ImagePath = "deadrising", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //  CAPCOM CO., LTD.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Dragon's Dogma: Dark Arisen", Genre = "RPG", StudioId = 5, ReleaseDate = DateTime.Parse("2016-01-15"), Price = 41, ImagePath = "dragonsdogmadarkarisen", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //  CAPCOM CO., LTD.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Resident Evil / biohazard HD REMASTER", Genre = "Horror", StudioId = 5, ReleaseDate = DateTime.Parse("2015-01-20"), Price = 28, ImagePath = "residentevilbiohazardhdremaster", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Codemasters
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "F1 2018", Genre = "Racing", StudioId = 6, ReleaseDate = DateTime.Parse("2018-08-24"), Price = 16, ImagePath = "f12018", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Codemasters
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "DiRT Rally", Genre = "Racing", StudioId = 6, ReleaseDate = DateTime.Parse("2015-12-07"), Price = 43, ImagePath = "dirtrally", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Codemasters
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "GRID 2", Genre = "Racing", StudioId = 6, ReleaseDate = DateTime.Parse("2013-05-28"), Price = 32, ImagePath = "grid2", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Codemasters
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Hospital Tycoon", Genre = "Simulation", StudioId = 6, ReleaseDate = DateTime.Parse("2007-06-05"), Price = 5, ImagePath = "hospitaltycoon", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Codemasters
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Toybox Turbos", Genre = "Racing", StudioId = 6, ReleaseDate = DateTime.Parse("2014-11-11"), Price = 16, ImagePath = "toyboxturbos", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Codemasters
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "GRID", Genre = "Racing", StudioId = 6, ReleaseDate = DateTime.Parse("2019-10-11"), Price = 65, ImagePath = "grid", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Codemasters
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Micro Machines World Series", Genre = "Racing", StudioId = 6, ReleaseDate = DateTime.Parse("2017-06-30"), Price = 32, ImagePath = "micromachinesworldseries", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Codemasters
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "F1 RACE STARS", Genre = "Racing", StudioId = 6, ReleaseDate = DateTime.Parse("2012-11-15"), Price = 4, ImagePath = "f1racestars", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Codemasters
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "GRID Autosport", Genre = "Racing", StudioId = 6, ReleaseDate = DateTime.Parse("2014-06-27"), Price = 43, ImagePath = "gridautosport", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            // Codemasters
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "DiRT 4", Genre = "Racing", StudioId = 6, ReleaseDate = DateTime.Parse("2017-06-09"), Price = 65, ImagePath = "dirt4", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //PlayWay S.A.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Cooking Simulator", Genre = "Simulation", StudioId = 7, ReleaseDate = DateTime.Parse("2019-06-06"), Price = 18, ImagePath = "cookingsimulator", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //PlayWay S.A.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Bad Dream: Coma", Genre = "Horror", StudioId = 7, ReleaseDate = DateTime.Parse("2017-03-09"), Price = 10, ImagePath = "baddreamcoma", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //PlayWay S.A.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Avenger Bird", Genre = "Platformer", StudioId = 7, ReleaseDate = DateTime.Parse("2017-01-05"), Price = 4, ImagePath = "avengerbird", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //PlayWay S.A.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Camper Jumper Simulator", Genre = "Racing", StudioId = 7, ReleaseDate = DateTime.Parse("2017-01-02"), Price = 3, ImagePath = "camperjumpersimulator", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //PlayWay S.A.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Phantaruk", Genre = "Horror", StudioId = 7, ReleaseDate = DateTime.Parse("2016-08-16"), Price = 5, ImagePath = "phantaruk", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //PlayWay S.A.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Street Arena", Genre = "Racing", StudioId = 7, ReleaseDate = DateTime.Parse("2015-07-10"), Price = 7, ImagePath = "streetarena", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //PlayWay S.A.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "UBOAT", Genre = "Simulation", StudioId = 7, ReleaseDate = DateTime.Parse("2019-04-30"), Price = 24, ImagePath = "uboat", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });

            //PlayWay S.A.
            context.Games.AddOrUpdate(x => new { x.GameName, x.StudioId },
                new Models.Game() { GameName = "Demolish & Build 2018", Genre = "Simulation", StudioId = 7, ReleaseDate = DateTime.Parse("2018-03-08"), Price = 11, ImagePath = "demolishbuild2018", Rating = Math.Round((double)rnd.Next(100,501)/100 * 2, MidpointRounding.AwayFromZero) / 2 });
            context.SaveChanges();
        }

        public void AddUsers(VGSDbContext context)
        {
            context.Users.AddOrUpdate(x => x.UserName, new User() { UserName = "Kamea", Age = 23, Email = "kamea@gmail.com", Balance = 150, Password = "369", UserType = 1 });
            context.Users.AddOrUpdate(x => x.UserName, new User() { UserName = "Lior", Age = 23, Email = "lior@gmail.com", Balance = 0, Password = "12", UserType = 1 });
            context.Users.AddOrUpdate(x => x.UserName, new User() { UserName = "Jonathan", Age = 23, Email = "jo@gmail.com", Balance = 200, Password = "12", UserType = 1 });
            context.Users.AddOrUpdate(x => x.UserName, new User() { UserName = "Ori", Age = 23, Email = "ori@gmail.com", Balance = 21, Password = "12", UserType = 1 });
            context.Users.AddOrUpdate(x => x.UserName, new User() { UserName = "User", Age = 23, Email = "user@gmail.com", Balance = 151, Password = "12", UserType = 0 });
            context.SaveChanges();
        }

        public void AddUserGames(VGSDbContext context)
        {
            HashSet<UserGame> userGamesSet = new HashSet<UserGame>(new SynonymComparer());
            Random rnd = new Random();
            int count=0;
            while (count<NUM_OF_USERGAMES)
            {
                UserGame userGame=new UserGame() {UserId=rnd.Next(1,NUM_OF_USERS+1),GameId=rnd.Next(1,NUM_OF_GAMES)};
                if (userGamesSet.Add(userGame))
                {
                    context.UserGames.AddOrUpdate(x => new { x.GameId, x.UserId }, userGame);
                    count++;
                }
            }
            context.SaveChanges();
        }

        public class SynonymComparer : IEqualityComparer<UserGame>
        {
            public bool Equals(UserGame one, UserGame two)
            {
                // Adjust according to requirements.
                return one.GameId==two.GameId && one.UserId==two.UserId;

            }

            public int GetHashCode(UserGame item)
            {
                return StringComparer.InvariantCultureIgnoreCase
                                     .GetHashCode(item.UserId+" "+item.GameId);

            }
        }
    }
}
