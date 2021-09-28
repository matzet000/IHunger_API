using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.Domain.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Service
{
    public class CommentService : BaseService, ICommentService
    {

        public CommentService(
            IUnitOfWork unitOfWork,
            INotifier notifier) : base(notifier, unitOfWork)
        {

        }

        public async Task<Comment> Create(Guid idRestaurant, Comment comment)
        {
            if (!Validate(new CommentValidation(), comment)) return null;

            var restaurantBD = await _unitOfWork
                .RepositoryFactory
                .RestaurantRepository
                .GetById(idRestaurant);

            if (restaurantBD == null)
            {
                NotifyError("There is no restaurant");
                return await Task.FromResult<Comment>(null);
            }

            await _unitOfWork
                .RepositoryFactory
                .CommentRepository
                .Add(comment);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(comment);
            }

            NotifyError("Error inserting entity");
            return await Task.FromResult<Comment>(null);
        }

        public async Task<Comment> GetById(Guid idRestaurant, Guid idComment)
        {
            return await _unitOfWork
                .RepositoryFactory
                .CommentRepository
                .GetById(idRestaurant, idComment);
        }

        public async Task<List<Comment>> GetAll(Guid idRestaurant)
        {
            return await _unitOfWork
                .RepositoryFactory
                .CommentRepository
                .GetAll(idRestaurant);
        }

        public async Task<Comment> Update(Guid idRestaurant, Guid idComment, Comment comment)
        {
            if (!Validate(new CommentValidation(), comment)) return null;

            var commentDb = await _unitOfWork
                .RepositoryFactory
                .CommentRepository
                .GetById(idRestaurant, idComment);

            if (commentDb == null)
            {
                NotifyError("Not found");
                return await Task.FromResult<Comment>(null);
            }

            if (commentDb.Starts != comment.Starts)
            {
                commentDb.Starts = comment.Starts;
            }

            if (commentDb.Text != comment.Text)
            {
                commentDb.Text = comment.Text;
            }


            _unitOfWork
                .RepositoryFactory
                .CommentRepository
                .Update(commentDb);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(commentDb);
            }

            NotifyError("Error deleting entity");
            return await Task.FromResult<Comment>(null);
        }

        public async Task<Comment> Delete(Guid idRestaurant, Guid idComment)
        {
            var comment = await _unitOfWork
                .RepositoryFactory
                .CommentRepository
                .GetById(idRestaurant, idComment);

            if (comment == null)
            {
                NotifyError("Not found");
                return await Task.FromResult<Comment>(null);
            }

            _unitOfWork
                .RepositoryFactory
                .CommentRepository
                .Remove(idComment);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(comment);
            }

            NotifyError("Error deleting entity");
            return await Task.FromResult<Comment>(null);
        }
    }
}
