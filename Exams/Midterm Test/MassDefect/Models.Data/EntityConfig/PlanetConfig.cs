namespace MassDefect.Data.EntityConfig
{
    using MassDefect.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PlanetConfig : IEntityTypeConfiguration<Planet>
    {
        public void Configure(EntityTypeBuilder<Planet> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.HasOne(p => p.Sun)
                .WithMany(s => s.Planets)
                .HasForeignKey(p => p.SunId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.SolarSystem)
                .WithMany(s => s.Planets)
                .HasForeignKey(p => p.SolarSystemId);
        }
    }
}