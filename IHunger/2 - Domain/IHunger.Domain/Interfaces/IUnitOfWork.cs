using IHunger.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        void Dispose();

        IRepositoryFactory RepositoryFactory { get; }
    }
}
