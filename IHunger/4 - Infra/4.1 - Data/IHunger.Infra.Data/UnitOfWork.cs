using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Repository;
using IHunger.Infra.Data.Context;
using IHunger.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataIdentityDbContext _dbContext;
        public IRepositoryFactory RepositoryFactory { get; }

        public UnitOfWork(DataIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
            RepositoryFactory = new RepositoryFactory(_dbContext);
        }

        public async Task<bool> Commit()
        {
            var result = await _dbContext.SaveChangesAsync();
            return await Task.FromResult(result > 0);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
