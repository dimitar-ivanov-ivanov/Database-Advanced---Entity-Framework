namespace PlanetHunters.Data
{
    using Microsoft.EntityFrameworkCore;
    using PlanetHunters.Models;

    public class PlanetHuntersContext : DbContext
    {
        public DbSet<Astronomer> Astronomers { get; set; }
        public DbSet<PioneerDiscovery> PioneersDiscoveries { get; set; }
        public DbSet<ObserverDiscovery> ObserversDiscoveries { get; set; }
        public DbSet<Discovery> Discoveries { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<StarSystem> StarSystems { get; set; }
        public DbSet<Telescope> Telescopes { get; set; }

        public PlanetHuntersContext() { }

        public PlanetHuntersContext(DbContextOptions options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.Connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ObserverDiscovery>()
                .HasKey(ob => new { ob.DiscoveryId, ob.ObserverId });
            builder.Entity<PioneerDiscovery>()
             .HasKey(ob => new { ob.DiscoveryId, ob.PioneerId });

            builder.Entity<ObserverDiscovery>()
                .HasOne(ob => ob.Observer)
                .WithMany(ob => ob.ObserverDiscoveries)
                .HasForeignKey(od => od.ObserverId);

            builder.Entity<ObserverDiscovery>()
               .HasOne(ob => ob.Discovery)
               .WithMany(ob => ob.Observers)
               .HasForeignKey(od => od.DiscoveryId);

            builder.Entity<PioneerDiscovery>()
               .HasOne(ob => ob.Pioneer)
               .WithMany(ob => ob.PioneeringDiscoveries)
               .HasForeignKey(od => od.PioneerId);

            builder.Entity<PioneerDiscovery>()
               .HasOne(ob => ob.Discovery)
               .WithMany(ob => ob.Pioneers)
               .HasForeignKey(od => od.DiscoveryId);
        }
    }
}
