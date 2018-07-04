namespace MassDefect.Data.EntityConfig
{
    using MassDefect.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AnomalyConfig : IEntityTypeConfiguration<Anomaly>
    {
        public void Configure(EntityTypeBuilder<Anomaly> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(a => a.HomePlanet)
                .WithMany(p => p.OriginAnomalies)
                .HasForeignKey(a => a.HomePlanetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.TeleportPlanet)
              .WithMany(p => p.TeleportAnomalies)
              .HasForeignKey(a => a.TeleportPlanetId);
        }
    }
}