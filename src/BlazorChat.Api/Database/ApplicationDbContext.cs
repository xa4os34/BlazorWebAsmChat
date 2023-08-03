using BlazorChat.Api.Database.Entities.Chatting;
using Microsoft.EntityFrameworkCore;

namespace BlazorChat.Api.Database;

public class ApplicationDbContext : DbContextBase
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<MessageEntity> Messages { get; protected set; } = null!;
}
