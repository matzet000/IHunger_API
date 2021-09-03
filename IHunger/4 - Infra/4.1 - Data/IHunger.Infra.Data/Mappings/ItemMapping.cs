using IHunger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.Data.Mappings
{
    public class ItemMapping : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Price)
               .IsRequired()
               .HasColumnType("DECIMAL");

            builder.Property(p => p.Quantity)
               .IsRequired()
               .HasColumnType("integer");
        }
    }
}
