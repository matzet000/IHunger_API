using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.Domain.Models.Validations;
using IHunger.Infra.CrossCutting.Filters;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IHunger.Service
{
    public class CategoryRestaurantService : BaseService, ICategoryRestaurantService
    {

        public CategoryRestaurantService(
            IUnitOfWork unitOfWork, 
            INotifier notifier) : base(notifier, unitOfWork)
        {

        }

        public async Task<CategoryRestaurant> Create(CategoryRestaurant categoryRestaurant)
        {
            if (!Validate(new CategoryRestaurantValidation(), categoryRestaurant)) return null;

            var categoryRestaurantDb = await _unitOfWork
                .RepositoryFactory
                .CategoryRestaurantRepository
                .Search(x => x.Name == categoryRestaurant.Name);

            if (categoryRestaurantDb != null && categoryRestaurantDb.Any())
            {
                NotifyError("Already exists CategoryRestaurant the same name");
                return await Task.FromResult<CategoryRestaurant>(null);
            }

            await _unitOfWork
                .RepositoryFactory
                .CategoryRestaurantRepository
                .Add(categoryRestaurant);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(categoryRestaurant);
            }

            NotifyError("Error inserting entity");
            return await Task.FromResult<CategoryRestaurant>(null);
        }

        public async Task<List<CategoryRestaurant>> GetAll()
        {
            return await _unitOfWork
                .RepositoryFactory
                .CategoryRestaurantRepository
                .GetAll();
        }

        public async Task<List<CategoryRestaurant>> GetAllWithFilter(CategoryRestaurantFilter CategoryRestaurantFilter)
        {
            Expression<Func<CategoryRestaurant, bool>> filter = null;
            Func<IQueryable<CategoryRestaurant>, IOrderedQueryable<CategoryRestaurant>> ordeBy = null;
            int? skip = null;
            int? take = 25;

            if (!string.IsNullOrWhiteSpace(CategoryRestaurantFilter.Name))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<CategoryRestaurant>(true);
                }

                filter = filter.And(x => x.Name == CategoryRestaurantFilter.Name);
            }

            if (!string.IsNullOrWhiteSpace(CategoryRestaurantFilter.Description))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<CategoryRestaurant>(true);
                }

                filter = filter.And(x => x.Description == CategoryRestaurantFilter.Description);
            }

            if (!string.IsNullOrWhiteSpace(CategoryRestaurantFilter.Order))
            {
                switch (CategoryRestaurantFilter.Order)
                {
                    case "Id":
                        ordeBy = x => x.OrderBy(n => n.Id);
                        break;
                    case "Name":
                        ordeBy = x => x.OrderBy(n => n.Name);
                        break;
                    case "Description":
                        ordeBy = x => x.OrderBy(n => n.Description);
                        break;
                    case "CreatedAt":
                        ordeBy = x => x.OrderBy(n => n.CreatedAt);
                        break;
                }
            }

            if (CategoryRestaurantFilter.Id != Guid.Empty)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<CategoryRestaurant>(true);
                }

                filter = filter.And(x => x.Id == CategoryRestaurantFilter.Id);
            }

            if (CategoryRestaurantFilter.CreatedAt.Year > 1)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<CategoryRestaurant>(true);
                }

                filter = filter.And(x => x.Id == CategoryRestaurantFilter.Id);
            }

            if (CategoryRestaurantFilter.PageIndex != null)
            {
                if (CategoryRestaurantFilter.PageIndex > 1)
                {
                    skip = CategoryRestaurantFilter.PageIndex * 25;
                }
            }

            if (CategoryRestaurantFilter.PageSize != null)
            {
                if (CategoryRestaurantFilter.PageSize > 0)
                {
                    take = CategoryRestaurantFilter.PageSize;
                }
            }

            return await _unitOfWork
                .RepositoryFactory
                .CategoryRestaurantRepository
                .Search(filter, ordeBy, skip, take);
        }

        public async Task<CategoryRestaurant> GetById(Guid id)
        {
            return await _unitOfWork
                .RepositoryFactory
                .CategoryRestaurantRepository
                .GetById(id);
        }

        public async Task<CategoryRestaurant> Update(CategoryRestaurant categoryRestaurant)
        {
            if (!Validate(new CategoryRestaurantValidation(), categoryRestaurant)) return null;

            var categoryRestaurantDb = await _unitOfWork
                .RepositoryFactory
                .CategoryRestaurantRepository
                .GetById(categoryRestaurant.Id);

            if (categoryRestaurantDb == null)
            {
                NotifyError("Not found");
                return await Task.FromResult<CategoryRestaurant>(null);
            }
            
            if(categoryRestaurantDb.Name != categoryRestaurant.Name)
            {
                categoryRestaurantDb.Name = categoryRestaurant.Name;
            }

            if (categoryRestaurantDb.Description != categoryRestaurant.Description)
            {
                categoryRestaurantDb.Description = categoryRestaurant.Description;
            }

            _unitOfWork
                .RepositoryFactory
                .CategoryRestaurantRepository
                .Update(categoryRestaurantDb);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(categoryRestaurantDb);
            }

            NotifyError("Error deleting entity");
            return await Task.FromResult<CategoryRestaurant>(null);
        }

        public async Task<CategoryRestaurant> Delete(Guid id)
        {
            var categoryRestaurantDb = await _unitOfWork
                .RepositoryFactory
                .CategoryRestaurantRepository
                .GetById(id);

            if (categoryRestaurantDb == null)
            {
                NotifyError("Not found");
                return await Task.FromResult<CategoryRestaurant>(null);
            }

            _unitOfWork
                .RepositoryFactory
                .CategoryRestaurantRepository
                .Remove(id);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(categoryRestaurantDb);
            }

            NotifyError("Error deleting entity");
            return await Task.FromResult<CategoryRestaurant>(null);
        }
    }
}
