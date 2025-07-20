using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<PageEntity> Pages { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PageEntity>()
                .HasIndex(p => new { p.ParentId, p.Slug })
                .IsUnique(); // Уникальность слага в пределах родителя

            modelBuilder.Entity<PageEntity>()
                .HasMany(p => p.Children)
                .WithOne(p => p.Parent)
                .HasForeignKey(p => p.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
