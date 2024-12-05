using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserServiceAPI.Domain.Entities;

namespace UserServiceAPI.Domain.Configurations
{
    /// <summary>
    /// Конфигурация для сущности ConfirmCode.
    /// </summary>
    public class ConfirmCodeConfiguration : IEntityTypeConfiguration<ConfirmCode>
    {
        public void Configure(EntityTypeBuilder<ConfirmCode> builder)
        { 
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(155);
            builder.Property(x => x.Code)
                .IsRequired();
            builder.Property(x => x.ExpiryDate);
            builder.HasIndex(x => x.Email)
               .IsUnique();


        }
    }
}
