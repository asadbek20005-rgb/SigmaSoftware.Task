using Microsoft.EntityFrameworkCore;
using SigmaSoftware.Data.Entites;

namespace SigmaSoftware.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; } 
}
