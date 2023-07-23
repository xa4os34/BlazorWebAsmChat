using Microsoft.EntityFrameworkCore;

namespace BlazorChat.Api.Database;

public class ApplicationDbContext : DbContextBase
{
    public ApplicationDbContext(
        DbContextOptions options) : base(options)
    {
    }
}
