using IHunger.Domain.Interfaces.Repository;
using IHunger.Domain.Models;
using IHunger.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.Data.Repository
{
    public class CategoryProductRepository : Repository<CategoryProduct>, ICategoryProductRepository
    {
        public CategoryProductRepository(DataIdentityDbContext db) : base(db)
        {
        }
    }
}
