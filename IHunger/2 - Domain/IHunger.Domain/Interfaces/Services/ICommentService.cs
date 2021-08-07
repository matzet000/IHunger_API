using IHunger.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Domain.Interfaces.Services
{
    public interface ICommentService
    {
        Task<Comment> Create(Guid idRestaurant, Comment comment);
        Task<Comment> GetById(Guid idRestaurant, Guid idComment);
        Task<List<Comment>> GetAll(Guid idRestaurant);
        Task<Comment> Update(Guid idRestaurant, Guid idComment, Comment comment);
        Task<Comment> Delete(Guid idRestaurant, Guid idComment);
    }
}
