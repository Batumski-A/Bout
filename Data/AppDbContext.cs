using Bout_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Bout_2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Bout> Bouts { get; set; }
    }
}
