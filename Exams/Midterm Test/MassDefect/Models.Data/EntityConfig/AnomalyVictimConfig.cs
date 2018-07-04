namespace MassDefect.Data.EntityConfig
{
    using MassDefect.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AnomalyVictimConfig : IEntityTypeConfiguration<AnomalyVictim>
    {
        public void Configure(EntityTypeBuilder<AnomalyVictim> builder)
        {
            builder.HasKey(av => new { av.AnomalyId, av.VictimId });
            builder.HasOne(av => av.Victim)
                .WithMany(v => v.AnomalyVictims)
                .HasForeignKey(av => av.VictimId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(av => av.Anomaly)
               .WithMany(a => a.AnomalyVictims)
               .HasForeignKey(av => av.AnomalyId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}