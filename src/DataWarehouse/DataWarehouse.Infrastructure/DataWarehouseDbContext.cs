using DataWarehouse.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataWarehouse.Infrastructure;

public class DataWarehouseDbContext(DbContextOptions<DataWarehouseDbContext> options) : DbContext(options)
{
    public DbSet<GameEntity> Games { get; set; }

    public DbSet<ReviewEntity> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameEntity>(entity =>
        {
            entity.HasKey(g => g.Id);

            entity.Property(g => g.Name)
                .IsRequired();

            entity.Property(g => g.ReleaseDate);

            entity.Property(g => g.Genres)
                .HasConversion(
                    genres => string.Join(',', genres),
                    genres => genres.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                )
                .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                    (genres1, genres2) => genres1.SequenceEqual(genres2),
                    genres => genres.Aggregate(0, (a, g) => HashCode.Combine(a, g.GetHashCode())),
                    genres => genres.ToList()));

            entity.HasMany(g => g.Reviews)
                .WithOne()
                .HasForeignKey(r => r.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ReviewEntity>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.Content)
                .HasMaxLength(1000);

            entity.Property(r => r.Rating)
                .IsRequired();

            entity.HasOne<GameEntity>()
                .WithMany(g => g.Reviews)
                .HasForeignKey(r => r.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}