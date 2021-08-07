using IHunger.Domain.Interfaces.Repository;
using IHunger.Domain.Models;
using IHunger.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Infra.Data.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DataIdentityDbContext db) : base(db)
        {
        }

        public virtual async Task<Comment> GetById(Guid idRestaurant, Guid idComment)
        {
            return await DbSet
                .AsNoTracking()
                .Where(x => x.IdRestaurant == idRestaurant && x.Id == idComment)
                .FirstOrDefaultAsync();
        }

        public virtual async Task<List<Comment>> GetAll(Guid idRestaurant)
        {
            return await DbSet
                .AsNoTracking()
                .Where(x => x.IdRestaurant == idRestaurant)
                .ToListAsync();
        }
    }
}
