namespace Instagraph.Data
{
    using Microsoft.EntityFrameworkCore;
    using Instagraph.Models;

    public class InstagraphContext : DbContext
    {
        public InstagraphContext() { }

        public InstagraphContext(DbContextOptions options)
        : base(options) { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            builder.Entity<User>()
               .HasMany(u => u.Comments)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Post>()
               .HasOne(p => p.Picture)
               .WithMany(p => p.Posts)
               .HasForeignKey(p => p.PictureId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserFollowing>()
                .HasKey(uf => new { uf.FollowingId, uf.UserId });
            builder.Entity<UserFollower>()
                .HasKey(uf => new { uf.FollowerId, uf.UserId });

            builder.Entity<User>()
                .HasMany(u => u.Followers)
                .WithOne(f => f.Follower)
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
              .HasMany(u => u.Following)
              .WithOne(f => f.Following)
              .HasForeignKey(f => f.FollowingId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}