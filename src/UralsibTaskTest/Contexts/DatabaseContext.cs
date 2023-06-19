using Microsoft.EntityFrameworkCore;
using UralsibTaskTest.Contexts.Abstraction;
using UralsibTaskTest.Entities;

namespace UralsibTaskTest.Contexts;

public sealed class DatabaseContext: DbContext,IDatabaseContext
{
    public DbSet<Commit> Commits => Set<Commit>();
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Commit>()
            .HasIndex(p => p.Sha)
            .IsUnique();

        modelBuilder.Entity<Commit>()
            .HasIndex(p => p.Login);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}