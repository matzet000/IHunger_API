using IHunger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.Data.Mappings
{
    public class CouponMapping : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Code)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Value)
                .IsRequired();

            builder.Property(p => p.ExpireAt)
                .IsRequired();

            builder.HasMany(p => p.Orders)
               .WithOne(c => c.Coupon)
               .HasForeignKey(c => c.IdCoupon);
        }
    }
}
