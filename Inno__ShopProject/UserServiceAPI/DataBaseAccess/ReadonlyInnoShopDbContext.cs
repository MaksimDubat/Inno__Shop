using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserServiceAPI.Entities;

namespace UserServiceAPI.DataBaseAccess
{
    /// <summary>
    ///  Контекст БД для управления сущностями пользователя и ролей, где данные будут только читаться.
    /// </summary>
    public class ReadonlyInnoShopDbContext : IdentityDbContext<AppUsers, AppRoles, int>
    {
        public ReadonlyInnoShopDbContext(DbContextOptions<ReadonlyInnoShopDbContext> options)
           : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadonlyInnoShopDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
