using Microsoft.EntityFrameworkCore;

namespace Marketplace.Models.DB
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<ConfirmationCode> ConfirmationCodes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}