using HomeWork18.Domain;
using HomeWork18.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork18.Data
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext>options) : base(options) { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local);Database=HomeWork18SQL;Trusted_Connection=True;MultipleActiveResultSets=True");
        }
    }
}
