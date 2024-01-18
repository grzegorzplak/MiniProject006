using Microsoft.EntityFrameworkCore;

namespace MiniProject006.Models
{
    public class Context : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public Context(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
