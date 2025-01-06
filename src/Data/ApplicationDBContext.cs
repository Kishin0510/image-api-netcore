using image_api_netcore.src.Models;
using Microsoft.EntityFrameworkCore;

namespace image_api_netcore.src.Data
{
    public class ApplicationDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Post> Posts { get; set; } = null!;
    }
}