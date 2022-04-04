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
    public class CategoryProductService : BaseService, ICategoryProductService
    {
        public CategoryProductService(
            INotifier notifier, 
            IUnitOfWork unitOfWork) : base(notifier, unitOfWork)
        {
        }

        public async Task<CategoryProduct> Create(CategoryProduct categoryProduct)
        {
            if (!Validate(new CategoryProductValidation(), categoryProduct)) return null;

            var categoryProductBD =  await _unitOfWork
                .RepositoryFactory
                .CategoryProductRepository
                .Search(x => x.Name == categoryProduct.Name);

            if(categoryProductBD != null && categoryProductBD.Any())
            {
                NotifyError("Already exists categoryProduct the same name");
                return await Task.FromResult<CategoryProduct>(null);
            }

            await _unitOfWork
                .RepositoryFactory
                .CategoryProductRepository
                .Add(categoryProduct);

            if(await _unitOfWork.Commit())
            {
                return await Task.FromResult(categoryProduct);
            }

            NotifyError("Error inserting entity");
            return await Task.FromResult<CategoryProduct>(null);
        }

        public async Task<List<CategoryProduct>> GetAll()
        {
            return await _unitOfWork
                .RepositoryFactory
                .CategoryProductRepository
                .GetAll();
        }

        public async Task<List<CategoryProduct>> GetAllWithFilter(CategoryProductFilter categoryProductFilter)
        {
            Expression<Func<CategoryProduct, bool>> filter = null;
            Func<IQueryable<CategoryProduct>, IOrderedQueryable<CategoryProduct>> ordeBy = null;
            int? skip = null;
            int? take = 25;

            if (!string.IsNullOrWhiteSpace(categoryProductFilter.Name))
            {
                if(filter == null)
                {
                    filter = PredicateBuilder.New<CategoryProduct>(true);
                }

                filter = filter.And(x => x.Name == categoryProductFilter.Name || x.Name.Contains(categoryProductFilter.Name));
            }

            if (!string.IsNullOrWhiteSpace(categoryProductFilter.Description))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<CategoryProduct>(true);
                }

                filter = filter.And(x => x.Description == categoryProductFilter.Description || x.Name.Contains(categoryProductFilter.Description));
            }

            if (!string.IsNullOrWhiteSpace(categoryProductFilter.Order))
            {
                switch (categoryProductFilter.Order)
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
                    case "UpdatedAt":
                        ordeBy = x => x.OrderBy(n => n.CreatedAt);
                        break;
                }
            }

            if(categoryProductFilter.Id != Guid.Empty)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<CategoryProduct>(true);
                }

                filter = filter.And(x => x.Id == categoryProductFilter.Id);
            }

            if (categoryProductFilter.CreatedAt.Year > 1)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<CategoryProduct>(true);
                }

                filter = filter.And(x => x.Id == categoryProductFilter.Id);
            }

            if (categoryProductFilter.PageIndex != null)
            {
                if(categoryProductFilter.PageIndex > 1)
                {
                    if(categoryProductFilter.PageSize > 0)
                    {
                        skip = categoryProductFilter.PageIndex * categoryProductFilter.PageSize;
                    }
                    else
                    {
                        skip = categoryProductFilter.PageIndex * 10;
                    }
                }
            }

            if (categoryProductFilter.PageSize != null)
            {
                if (categoryProductFilter.PageSize > 0)
                {
                    take = categoryProductFilter.PageSize;
                }
            }

            return await _unitOfWork
                .RepositoryFactory
                .CategoryProductRepository
                .Search(filter, ordeBy, skip, take);
        }

        public async Task<CategoryProduct> GetById(Guid id)
        {
            return await _unitOfWork
                .RepositoryFactory
                .CategoryProductRepository
                .GetById(id);
        }

        public async Task<CategoryProduct> Update(CategoryProduct categoryProduct)
        {
            if (!Validate(new CategoryProductValidation(), categoryProduct)) return null;

            var categoryProductDb = await _unitOfWork
                .RepositoryFactory
                .CategoryProductRepository
                .GetById(categoryProduct.Id);

            if (categoryProductDb == null)
            {
                NotifyError("Not found");
                return await Task.FromResult<CategoryProduct>(null);
            }

            if (categoryProductDb.Name != categoryProduct.Name)
            {
                categoryProductDb.Name = categoryProduct.Name;
            }

            if (categoryProductDb.Description != categoryProduct.Description)
            {
                categoryProductDb.Description = categoryProduct.Description;
            }

            _unitOfWork
                .RepositoryFactory
                .CategoryProductRepository
                .Update(categoryProductDb);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult<CategoryProduct>(categoryProductDb);
            }

            NotifyError("Error deleting entity");
            return await Task.FromResult<CategoryProduct>(null);
        }

        public async Task<CategoryProduct> Delete(Guid id)
        {
            var categoryProduct = await _unitOfWork
                .RepositoryFactory
                .CategoryProductRepository
                .GetById(id);

            if(categoryProduct == null)
            {
                NotifyError("Not found");
                return await Task.FromResult<CategoryProduct>(null);
            }

            _unitOfWork
                .RepositoryFactory
                .CategoryProductRepository
                .Remove(id);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(categoryProduct);
            }

            NotifyError("Error deleting entity");
            return await Task.FromResult<CategoryProduct>(null);
        }
    }
}
