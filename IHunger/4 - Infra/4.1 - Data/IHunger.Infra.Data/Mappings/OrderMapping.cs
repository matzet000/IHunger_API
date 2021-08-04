using IHunger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Price)
               .IsRequired()
               .HasColumnType("DECIMAL");

            builder.HasMany(r => r.Items)
               .WithOne(c => c.Order)
               .HasForeignKey(c => c.IdOrder);
        }
    }
}
