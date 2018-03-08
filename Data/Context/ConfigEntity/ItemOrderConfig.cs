using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context.ConfigEntity
{
    internal class ItemOrderConfig : IEntityTypeConfiguration<ItemOrder>
    {
        public void Configure(EntityTypeBuilder<ItemOrder> entityType)
        {
            entityType.HasKey(f => f.Id);

            entityType.Property(f => f.ProductId)
                .IsRequired()
                .HasMaxLength(30);

            entityType.Property(f => f.Quantity)
                .IsRequired();

            entityType.Property(f => f.Registration)
                .IsRequired();
        }
    }
}