using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductServiceAPI.Entities;

namespace ProductServiceAPI.Cofigurations
{
    /// <summary>
    /// Конфигурация сущности продукта.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.ToTable("Product");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("decimal(5,2)");
            builder.Property(x => x.IsAvaliable)
                .IsRequired();
            builder.Property(x => x.IsAvaliable)
                .IsRequired(true);
            builder.Property(x => x.CreatedAt)
                .IsRequired();
            builder.HasIndex(x => x.UserId)
                .IsUnique();
        }
    }
}
