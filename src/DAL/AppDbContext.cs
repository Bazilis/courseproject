using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Collection>? Collections { get; set; }
        public DbSet<Item>? Items { get; set; }
        public DbSet<Tag>? Tags { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<UserLike>? UserLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Collection>()
                .Property(c => c.CollectionType)
                .HasConversion<string>();
        }
    }
}
