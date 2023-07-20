using Microsoft.EntityFrameworkCore;

namespace BlazorChat.Api.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions options) : base(options)
    {
    }


}
