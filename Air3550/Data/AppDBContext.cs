using Microsoft.EntityFrameworkCore;

using Air3550.Models;

namespace Air3550.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightBookInfo> FlightBookInfos { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<SystemInfo> SystemInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Air3550.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
