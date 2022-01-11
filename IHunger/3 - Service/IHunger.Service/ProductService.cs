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
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Service
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(
            INotifier notifier, 
            IUnitOfWork unitOfWork) : base(notifier, unitOfWork)
        {
        }

        public async Task<Product> Create(Product product)
        {
            if (!Validate(new ProductValidation(), product)) return null;

            var restaurant = await _unitOfWork
                .RepositoryFactory
                .RestaurantRepository
                .GetById(product.IdRestaurant);

            if (restaurant == null)
            {
                NotifyError("Not fround restaurant");
                return await Task.FromResult<Product>(null);
            }

            var categoryProduct = await _unitOfWork
                .RepositoryFactory
                .CategoryProductRepository
                .GetById(product.IdCategoryProduct);

            if (categoryProduct == null)
            {
                NotifyError("Not fround categoryProduct");
                return await Task.FromResult<Product>(null);
            }

            await _unitOfWork
                .RepositoryFactory
                .ProductRepository
                .Add(product);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(product);
            }

            NotifyError("Error inserting entity");
            return await Task.FromResult<Product>(null);
        }

        public async Task<List<Product>> GetAllWithFilter(ProductFilter productFilter)
        {
            Expression<Func<Product, bool>> filter = null;
            Func<IQueryable<Product>, IOrderedQueryable<Product>> ordeBy = null;
            int? skip = null;
            int? take = 25;

            if (!string.IsNullOrWhiteSpace(productFilter.Name))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Product>(true);
                }

                filter = filter.And(x => x.Name == productFilter.Name);
            }

            if (productFilter.Id != Guid.Empty)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Product>(true);
                }

                filter = filter.And(x => x.Id == productFilter.Id);
            }

            if (productFilter.IdCategoryProduct != Guid.Empty)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Product>(true);
                }

                filter = filter.And(x => x.CategoryProduct.Id == productFilter.IdCategoryProduct);
            }

            if (!string.IsNullOrWhiteSpace(productFilter.Description))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Product>(true);
                }

                filter = filter.And(x => x.Description == productFilter.Description);
            }

            if (productFilter.Kosher)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Product>(true);
                }

                filter = filter.And(x => x.Kosher == productFilter.Kosher);
            }

            if (productFilter.Vegan)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Product>(true);
                }

                filter = filter.And(x => x.Vegan == productFilter.Vegan);
            }

            if (productFilter.Vegetarian)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Product>(true);
                }

                filter = filter.And(x => x.Vegetarian == productFilter.Vegetarian);
            }

            if (!string.IsNullOrWhiteSpace(productFilter.NameCategoryProduct))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Product>(true);
                }

                filter = filter.And(x => x.CategoryProduct.Name == productFilter.NameCategoryProduct);
            }

            if (productFilter.CreatedAt.Year > 1)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Product>(true);
                }

                filter = filter.And(x => x.Id == productFilter.Id);
            }

            if (productFilter.PageIndex != null)
            {
                if (productFilter.PageIndex > 1)
                {
                    skip = productFilter.PageIndex * 25;
                }
            }

            if (productFilter.PageSize != null)
            {
                if (productFilter.PageSize > 0)
                {
                    take = productFilter.PageSize;
                }
            }

            if (!string.IsNullOrWhiteSpace(productFilter.Order))
            {
                switch (productFilter.Order)
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
                    case "Price":
                        ordeBy = x => x.OrderBy(n => n.Price);
                        break;
                    case "CreatedAt":
                        ordeBy = x => x.OrderBy(n => n.CreatedAt);
                        break;
                }
            }

            return await _unitOfWork
                .RepositoryFactory
                .ProductRepository
                .Search(
                    filter,
                    ordeBy,
                    skip,
                    take);
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _unitOfWork
                .RepositoryFactory
                .ProductRepository
                .GetById(id);
        }

        public async Task<Product> Update(Product product)
        {
            if (!Validate(new ProductValidation(), product)) return null;

            var productDB = await _unitOfWork
                .RepositoryFactory
                .ProductRepository
                .GetById(product.Id);

            if (productDB == null)
            {
                NotifyError("Not found");
                return await Task.FromResult<Product>(null);
            }

            if (product.CategoryProduct != null)
            {
                var categoryProduct = await _unitOfWork
                .RepositoryFactory
                .CategoryProductRepository
                .GetById(product.IdCategoryProduct);

                if (categoryProduct == null)
                {
                    NotifyError("Not fround categoryProduct");
                    return await Task.FromResult<Product>(null);
                }

            }

            if (productDB.Name != product.Name)
            {
                productDB.Name = product.Name;
            }

            if (productDB.Description != product.Description)
            {
                productDB.Description = product.Description;
            }

            if (productDB.Price != product.Price)
            {
                productDB.Price = product.Price;
            }

            if (productDB.Kosher != product.Kosher)
            {
                productDB.Kosher = product.Kosher;
            }

            if (productDB.Vegan != product.Vegan)
            {
                productDB.Vegan = product.Vegan;
            }

            if (productDB.Vegetarian != product.Vegetarian)
            {
                productDB.Vegetarian = product.Vegetarian;
            }

            if (productDB.Image != product.Image)
            {
                productDB.Image = product.Image;
            }

            _unitOfWork
                .RepositoryFactory
                .ProductRepository
                .Update(productDB);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(productDB);
            }

            NotifyError("Error deleting entity");
            return await Task.FromResult<Product>(null);
        }

        public async Task<Product> Delete(Guid id)
        {
            var product = await _unitOfWork
                .RepositoryFactory
                .ProductRepository
                .GetById(id);

            if (product == null)
            {
                NotifyError("Not found");
                return await Task.FromResult<Product>(null);
            }

            _unitOfWork
                .RepositoryFactory
                .ProductRepository
                .Remove(id);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(product);
            }

            NotifyError("Error deleting entity");
            return await Task.FromResult<Product>(null);
        }

        public async Task<List<Product>> GetByRestaurant(Guid id)
        {
            var restaurant = await _unitOfWork
                .RepositoryFactory
                .RestaurantRepository
                .GetById(id);

            if(restaurant == null)
            {
                NotifyError("Not fround Restaurant");
                return await Task.FromResult<List<Product>>(null);
            }

            return await _unitOfWork
                 .RepositoryFactory
                 .ProductRepository
                 .GetByRestaurant(id);
        }

        public async Task<Product> GetByRestaurantByIdProduct(Guid idRestaurant, Guid idProduct)
        {
            var restaurant = await _unitOfWork
                .RepositoryFactory
                .RestaurantRepository
                .GetById(idRestaurant);

            if (restaurant == null)
            {
                NotifyError("Not fround Restaurant");
                return await Task.FromResult<Product>(null);
            }

            return await _unitOfWork
                 .RepositoryFactory
                 .ProductRepository
                 .GetByRestaurantByIdProduct(idRestaurant, idProduct);
        }
    }
}
