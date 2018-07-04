namespace MassDefect.Data.EntityConfig
{
    using MassDefect.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SolarSystemConfig : IEntityTypeConfiguration<SolarSystem>
    {
        public void Configure(EntityTypeBuilder<SolarSystem> builder)
        {
            builder.HasKey(ss => ss.Id);
            builder.Property(ss => ss.Name).IsRequired();
        }
    }
}