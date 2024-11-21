using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProductServiceAPI.Entities;

namespace ProductServiceAPI.DataBase
{
    /// <summary>
    /// Контекст БД для работы с продуктами.
    /// </summary>
    public class MutableInnoShopProductDbContext : DbContext
    {
        public MutableInnoShopProductDbContext(DbContextOptions<MutableInnoShopProductDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MutableInnoShopProductDbContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public bool HasPeddingChanges()
        {
            return ChangeTracker.HasChanges();
        }
    }
    
}
