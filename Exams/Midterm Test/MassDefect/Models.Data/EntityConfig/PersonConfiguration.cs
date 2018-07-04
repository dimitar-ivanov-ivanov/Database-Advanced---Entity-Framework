namespace MassDefect.Data.EntityConfig
{
    using MassDefect.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.HasOne(p => p.HomePlanet)
                .WithMany(p => p.Persons);

        }
    }
}
