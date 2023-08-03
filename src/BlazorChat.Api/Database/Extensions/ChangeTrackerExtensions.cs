using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlazorChat.Api.Database.Extensions;

public static class ChangeTrackerExtensions
{
    public static IEnumerable<EntityEntry<TEntity>> GetEntriesByState<TEntity>(
        this ChangeTracker changeTracker,
        EntityState state) 
        where TEntity : class
    {
        return changeTracker.Entries<TEntity>()
            .Where((EntityEntry<TEntity> e) => e.State == state);
    }
}