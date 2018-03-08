using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context.ConfigEntity
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entityType)
        {
            entityType.HasKey(f => f.Id);

            entityType.Property(f => f.UserId)
                .IsRequired();

            entityType.Property(f => f.Registration)
                .IsRequired();
        }
    }
}