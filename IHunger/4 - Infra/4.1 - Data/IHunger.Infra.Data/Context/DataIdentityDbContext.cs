using IHunger.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IHunger.Infra.Data.Context
{
    public class DataIdentityDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DataIdentityDbContext(DbContextOptions<DataIdentityDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<OrderStatus> OrdersStatus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Itens { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Restaurant> Comments { get; set; }
        public DbSet<CategoryRestaurant> CategoryRestaurants { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<AddressRestaurant> AddressRestaurants { get; set; }
        public DbSet<AddressUser> AddressUsers { get; set; }
        public DbSet<ProfileUser> ProfileUsers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            //optionsBuilder.UseLazyLoadingProxies();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataIdentityDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<ZonedDateTime>(x => x.HaveConversion<ZonedDateTimeConverter>());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                    entry.Property("CreatedAt").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


    }

    internal class ZonedDateTimeConverter : ValueConverter<ZonedDateTime, LocalDateTime>
    {
        public ZonedDateTimeConverter() :
           base(v => v.WithZone(DateTimeZone.Utc).LocalDateTime, v => v.InUtc())
        {
        }
    }
}
