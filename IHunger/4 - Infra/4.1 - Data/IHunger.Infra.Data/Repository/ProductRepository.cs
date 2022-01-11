using IHunger.Domain.Interfaces.Repository;
using IHunger.Domain.Models;
using IHunger.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Infra.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataIdentityDbContext db) : base(db)
        {
        }

        public override async Task<Product> GetById(Guid id)
        {
            return await DbSet
                .Include(x => x.Restaurant)
                .Include(x => x.CategoryProduct)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetByRestaurant(Guid id)
        {
            return await DbSet
                .Include(x => x.Restaurant)
                .Include(x => x.CategoryProduct)
                .Where(x => x.IdRestaurant == id)
                .ToListAsync();
        }

        public async Task<Product> GetByRestaurantByIdProduct(Guid idRestaurant, Guid idProduct)
        {
            return await DbSet
                .Include(x => x.Restaurant)
                .Include(x => x.CategoryProduct)
                .Where(x => x.IdRestaurant == idRestaurant && x.Id == idProduct)
                .FirstOrDefaultAsync();
        }

        public override async Task<List<Product>> Search(
            Expression<Func<Product, bool>> predicate = null,
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            var query = DbSet.AsQueryable();

            // extract to method
            query = query.Include(x => x.Restaurant).Include(x => x.CategoryProduct);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (skip != null && skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null && take.HasValue)
            {
                query = query.Take(take.Value);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }
    }
}
