using IHunger.Domain.Interfaces.Repository;
using IHunger.Domain.Models;
using IHunger.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Infra.Data.Repository
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(DataIdentityDbContext db) : base(db)
        {
        }

        public override async Task<Restaurant> GetById(Guid id)
        {
            return await DbSet
                .Include(x => x.AddressRestaurant)
                .Include(x => x.CategoryRestaurant)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public override async Task<List<Restaurant>> Search(
            Expression<Func<Restaurant, bool>> predicate = null,
            Func<IQueryable<Restaurant>, IOrderedQueryable<Restaurant>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            var query = DbSet.AsQueryable();

            query = query.Include(x => x.AddressRestaurant).Include(x => x.CategoryRestaurant).Include(x => x.Comments);
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
