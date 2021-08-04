using IHunger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.Data.Mappings
{
    public class CategoryProductMapping : IEntityTypeConfiguration<CategoryProduct>
    {

        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");
        }
    }
}
