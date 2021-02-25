using System.Data.Entity;

namespace VGS.Models
{
    public class VGSDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
    }
}