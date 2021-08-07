using IHunger.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Domain.Interfaces.Repository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment> GetById(Guid idRestaurant, Guid idComment);
        Task<List<Comment>> GetAll(Guid idRestaurant);
    }
}
