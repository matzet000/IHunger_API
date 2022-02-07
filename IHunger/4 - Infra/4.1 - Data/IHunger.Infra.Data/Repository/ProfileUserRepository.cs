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
    public class ProfileUserRepository : Repository<ProfileUser>, IProfileUserRepository
    {
        public ProfileUserRepository(DataIdentityDbContext db) : base(db)
        {
        }

        public override async Task<ProfileUser> GetById(Guid id)
        {
            return await DbSet
                .Include(x => x.AddressUser)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

    }
}
