namespace Trucks.Data
{
    using Microsoft.EntityFrameworkCore;
    using Trucks.Data.Models;

    public class TrucksContext : DbContext
    {
        public TrucksContext()
        { 
        }

        public TrucksContext(DbContextOptions options)
            : base(options) 
        { 
        }

        public DbSet<Client> Clients { get; set; } = null!;

        public DbSet<Truck> Trucks { get; set; } = null!;

        public DbSet<Despatcher> Despatchers { get; set; } = null!;

        public DbSet<Truck> ClientsTrucks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString)
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientTruck>()
                .HasKey(x => new { x.ClientId, x.TruckId });
        }
    }
}
