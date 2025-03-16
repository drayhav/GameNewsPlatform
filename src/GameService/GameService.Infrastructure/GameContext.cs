using GameService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameService.Infrastructure
{
    public class GameContext(DbContextOptions<GameContext> options) : DbContext(options)
    {
        public DbSet<GameEntity> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameEntity>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
