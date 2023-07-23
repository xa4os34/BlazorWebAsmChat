using BlazorChat.Api.database.Extensions;
using BlazorChat.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlazorChat.Api.Database;

public abstract class DbContextBase : DbContext
{
    public DbContextBase(
        DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(Program).Assembly);
            
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        PreProcessChanges();
        return base.SaveChanges();
    }

    private void PreProcessChanges()
    {
        if (!ChangeTracker.HasChanges())
            return;

        UpdateDatedEntitiesData();
    }

    private void UpdateDatedEntitiesData()
    {
        ProcessEntriesWithState<IDatedEntity>(
            EntityState.Added, 
            (IDatedEntity entity) =>
            {
                entity.CreatedAt = DateTime.UtcNow;
            });

        ProcessEntriesWithState<IDatedEntity>(
            EntityState.Modified, 
            (IDatedEntity entity) =>
            {
                entity.ModifiedAt = DateTime.UtcNow;
            });
    }

    private void ProcessEntriesWithState<TEntity>(
        EntityState state, Action<TEntity> processor) 
        where TEntity : class
    {
        IEnumerable<EntityEntry<TEntity>> entries =
            ChangeTracker.GetEntriesByState<TEntity>(state);

        foreach (EntityEntry<TEntity> entry in entries)
            processor?.Invoke(entry.Entity);
    }
}