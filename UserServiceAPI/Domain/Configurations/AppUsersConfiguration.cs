using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserServiceAPI.Domain.Entities;

namespace UserServiceAPI.Domain.Configurations
{
    /// <summary>
    /// Конфигурация для сущности AppUsers.
    /// </summary>
    public class AppUsersConfiguration : IEntityTypeConfiguration<AppUsers>
    {
        public void Configure(EntityTypeBuilder<AppUsers> builder)
        {
            builder.ToTable("AppUsers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId)
                .IsRequired();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(155);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(x => x.PasswordHash)
                .IsRequired();
            builder.Property(x => x.Role)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();


        }


    }
}
