using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserServiceAPI.Entities;

namespace UserServiceAPI.Configurations
{
    /// <summary>
    /// Конфигурация для сущности LoginResponse.
    /// </summary>
    public sealed class LoginResponseConfiguration : IEntityTypeConfiguration<LoginResponse>
    {
       
        public void Configure(EntityTypeBuilder<LoginResponse> builder)
        {
            builder.ToTable("LoginResponse");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId);
            builder.Property(x => x.Token)
                .IsRequired()
                .HasMaxLength(1255);
        }
    }
}
