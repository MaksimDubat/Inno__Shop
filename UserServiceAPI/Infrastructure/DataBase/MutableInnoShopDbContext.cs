﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserServiceAPI.Domain.Entities;

namespace UserServiceAPI.Infrastructure.DataBase
{
    /// <summary>
    /// Контекст БД для управления сущностями.
    /// </summary>
    public class MutableInnoShopDbContext : IdentityDbContext<AppUsers, AppRoles, int>
    {
        public MutableInnoShopDbContext() : base() { }
        public MutableInnoShopDbContext(DbContextOptions<MutableInnoShopDbContext> options) : base(options) { }



        public DbSet<AppUsers> AppUsers { get; set; }
        public DbSet<LoginResponse> LoginResponses { get; set; }
        public DbSet<ConfirmCode> ConfirmCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MutableInnoShopDbContext).Assembly);
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
