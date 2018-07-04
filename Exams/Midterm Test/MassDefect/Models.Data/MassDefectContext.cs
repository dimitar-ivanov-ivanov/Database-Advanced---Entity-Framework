namespace MassDefect.Data
{
    using MassDefect.Data.EntityConfig;
    using MassDefect.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class MassDefectContext : DbContext
    {
        public DbSet<Anomaly> Anomalies { get; set; }
        public DbSet<AnomalyVictim> AnomalyVictims { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<SolarSystem> SolarSystems { get; set; }
        public DbSet<Star> Stars { get; set; }

        public MassDefectContext() { }

        public MassDefectContext(DbContextOptions options)
        :base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.Connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AnomalyConfig());
            builder.ApplyConfiguration(new PersonConfig());
            builder.ApplyConfiguration(new PlanetConfig());
            builder.ApplyConfiguration(new SolarSystemConfig());
            builder.ApplyConfiguration(new StarConfig());
            builder.ApplyConfiguration(new AnomalyVictimConfig());
        }

        public Planet FirstOrDefault()
        {
            throw new NotImplementedException();
        }
    }
}
