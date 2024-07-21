using Microsoft.EntityFrameworkCore;
using PruebaIngreso.Models;
using Task = PruebaIngreso.Models.Task;

namespace PruebaIngreso.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<auth> Logins { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}