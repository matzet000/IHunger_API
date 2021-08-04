using IHunger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.Data.Mappings
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Text)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Starts)
                .IsRequired()
                .HasColumnType("DECIMAL");
        }
    }
}
