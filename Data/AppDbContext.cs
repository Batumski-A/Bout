using Boat_2.Controllers;
using Boat_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Boat_2.Data
{
    public class AppDbContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Boat> Boats { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
