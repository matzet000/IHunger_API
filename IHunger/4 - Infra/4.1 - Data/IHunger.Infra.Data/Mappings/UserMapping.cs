using IHunger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.AddressUser)
                .WithOne(a => a.User)
                .HasForeignKey<User>(a => a.AddressUserId);

            builder.HasMany(u => u.Orders)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId);
        }
    }
}
