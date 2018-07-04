
namespace MassDefect.Data.EntityConfig
{
    using MassDefect.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StarConfig : IEntityTypeConfiguration<Star>
    {
        public void Configure(EntityTypeBuilder<Star> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasOne(s => s.SolarSystem)
                .WithMany(p => p.Stars)
                .HasForeignKey(s => s.SolarSystemId);
        }
    }
}
