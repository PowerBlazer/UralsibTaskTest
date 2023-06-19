using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UralsibTaskTest.Entities;

namespace UralsibTaskTest.Contexts.Abstraction;

public interface IDatabaseContext
{
    DbSet<Commit> Commits { get; }
    Task<int> SaveChangesAsync();
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}