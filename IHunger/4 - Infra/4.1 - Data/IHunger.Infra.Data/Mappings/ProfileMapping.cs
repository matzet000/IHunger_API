using IHunger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.Data.Mappings
{
    public class ProfileMapping : IEntityTypeConfiguration<ProfileUser>
    {
        public void Configure(EntityTypeBuilder<ProfileUser> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(80)");

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasColumnType("varchar(80)");

            builder.Property(p => p.BirthDate)
                .IsRequired(false);

            builder.Property(p => p.Type)
                .IsRequired();

            builder.HasMany(u => u.Orders)
               .WithOne(c => c.ProfileUser)
               .HasForeignKey(c => c.IdProfileUser)
               .IsRequired(false);

            builder.Property(x => x.IdAddressUser).IsRequired();

            builder.HasOne(x => x.AddressUser).WithMany().HasForeignKey(x => x.IdAddressUser);
        }
    }
}
