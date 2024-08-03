using Microsoft.EntityFrameworkCore;
using StealAllTheCats.Infrastructure.Context.Entities;

namespace StealAllTheCats.Infrastructure.Context;

public class DBContext : DbContext
{
    public DBContext() { }

    public DBContext(DbContextOptions<DBContext> options)
    : base(options) { }

    public virtual DbSet<Cat> Cats { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cat>(entity =>
        {
            entity.ToTable("Cats");

            entity.HasKey(e => e.ID);
            entity.Property(e => e.ID).ValueGeneratedOnAdd();

            entity.HasMany(d => d.Tags)
                 .WithMany(p => p.Cats);

        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tags");

            entity.HasKey(e => e.ID);
            entity.Property(e => e.ID).ValueGeneratedOnAdd();
        });
    }
}
