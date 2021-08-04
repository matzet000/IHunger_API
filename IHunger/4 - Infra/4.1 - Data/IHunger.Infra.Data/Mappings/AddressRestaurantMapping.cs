using IHunger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.Data.Mappings
{
    public class AddressRestaurantMapping : IEntityTypeConfiguration<AddressRestaurant>
    {
        public void Configure(EntityTypeBuilder<AddressRestaurant> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.City)
                .IsRequired()
                .HasColumnType("varchar(80)");

            builder.Property(p => p.County)
                .IsRequired()
                .HasColumnType("varchar(80)");

            builder.Property(p => p.District)
                .IsRequired()
                .HasColumnType("varchar(80)");

            builder.Property(p => p.Street)
                .IsRequired()
                .HasColumnType("varchar(80)");

            builder.Property(p => p.ZipCode)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.Property(p => p.Latitude)
                .HasColumnType("varchar(80)");

            builder.Property(p => p.Longitude)
                .HasColumnType("varchar(80)");
        }
    }
}
