using Microsoft.EntityFrameworkCore;

namespace Marketplace.Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
    }
}