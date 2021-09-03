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
              .HasColumnType("boolean");

            builder.Property(p => p.Vegan)
              .IsRequired()
              .HasColumnType("boolean");

            builder.Property(p => p.Vegetarian)
              .IsRequired()
              .HasColumnType("boolean");

            builder.Property(p => p.Image)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.HasMany(r => r.Itens)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.IdProduct);

            builder.Property(x => x.IdCategoryProduct).IsRequired();

            builder.HasOne(x => x.CategoryProduct).WithMany().HasForeignKey(x => x.IdCategoryProduct);
        }
    }
}
