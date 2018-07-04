namespace TeamBuilder.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TeamBuilder.Models;

    public class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(ut => new { ut.TeamId, ut.UserId });
            builder.HasOne(ut => ut.User)
                .WithMany(t => t.UserTeams)
                .HasForeignKey(t=>t.UserId);

            builder.HasOne(ut => ut.Team)
              .WithMany(t => t.UserTeams)
              .HasForeignKey(t => t.TeamId);
        }
    }
}
