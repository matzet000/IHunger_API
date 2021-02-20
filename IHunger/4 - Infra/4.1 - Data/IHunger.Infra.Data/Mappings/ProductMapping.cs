using IHunger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Description)
               .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(p => p.Price)
               .IsRequired()
               .HasColumnType("DECIMAL");

            builder.Property(p => p.Kosher)
              .IsRequired()
              .HasColumnType("bit");

            builder.Property(p => p.Vegan)
              .IsRequired()
              .HasColumnType("bit");

            builder.Property(p => p.Vegetarian)
              .IsRequired()
              .HasColumnType("bit");

            builder.Property(p => p.Image)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.HasOne(p => p.CategoryProduct)
                .WithOne(c => c.Product);

            builder.HasMany(r => r.Itens)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId);

            builder.ToTable("Products");
        }
    }
}
