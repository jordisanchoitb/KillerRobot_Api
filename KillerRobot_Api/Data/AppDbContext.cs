using KillerRobot_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace KillerRobot_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Scores> Scores { get; set; }
    }
}
