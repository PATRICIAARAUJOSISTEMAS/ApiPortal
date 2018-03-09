using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Context.ConfigEntity
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entityType)
        {
            entityType.HasKey(f => f.Id);

            entityType.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(30);

            entityType.Property(f => f.Price)
                .IsRequired();

            entityType.Property(f => f.Registration)
                .IsRequired();
        }
    }
}