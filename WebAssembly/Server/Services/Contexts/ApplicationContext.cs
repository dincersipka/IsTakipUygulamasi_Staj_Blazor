using Microsoft.EntityFrameworkCore;
using WebAssembly.Shared.Models;

namespace WebAssembly.Server.Services.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Message>? messages { get; set; }
        public DbSet<User>? users { get; set; }
        public DbSet<Role>? roles { get; set; }
        public DbSet<Department>? departments { get; set; }
        public DbSet<Statistic>? statistics { get; set; }
    }
}
