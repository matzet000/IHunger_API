using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.Domain.Models.Validations;
using IHunger.Infra.CrossCutting.Filters;
using LinqKit;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IHunger.Service
{
    public class RestaurantService : BaseService, IRestaurantService
    {
        public RestaurantService(
            INotifier notifier,
            IUnitOfWork unitOfWork) : base(notifier, unitOfWork)
        {

        }

        public async Task<Restaurant> Create(Restaurant restaurant)
        {
            if (!Validate(new RestaurantValidation(), restaurant)) return null;
            if (!Validate(new AddressRestaurantValidation(), restaurant.AddressRestaurant)) return null;

            var restaurantBD = await _unitOfWork
                .RepositoryFactory
                .RestaurantRepository
                .Search(x => x.Name == restaurant.Name);

            if (restaurantBD != null && restaurantBD.Any())
            {
                NotifyError("Already exists restaurant the same name");
                return await Task.FromResult<Restaurant>(null);
            }

            var categoryRestaurant = await _unitOfWork
                .RepositoryFactory
                .CategoryRestaurantRepository
                .GetById(restaurant.IdCategoryRestaurant);

            if (categoryRestaurant == null)
            {
                NotifyError("Not fround categoryRestaurant");
                return await Task.FromResult<Restaurant>(null);
            }

            await _unitOfWork
                .RepositoryFactory
                .RestaurantRepository
                .Add(restaurant);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(restaurant);
            }

            NotifyError("Error inserting entity");
            return await Task.FromResult<Restaurant>(null);
        }

        public async Task<List<Restaurant>> GetAllWithFilter(RestaurantFilter restaurantFilter)
        {
            Expression<Func<Restaurant, bool>> filter = null;
            Func<IQueryable<Restaurant>, IOrderedQueryable<Restaurant>> ordeBy = null;
            int? skip = null;
            int? take = 25;

            if (!string.IsNullOrWhiteSpace(restaurantFilter.Name))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Restaurant>(true);
                }

                filter = filter.And(x => x.Name == restaurantFilter.Name);
            }

            if (!string.IsNullOrWhiteSpace(restaurantFilter.Description))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Restaurant>(true);
                }

                filter = filter.And(x => x.Description == restaurantFilter.Description);
            }

            if (!string.IsNullOrWhiteSpace(restaurantFilter.CategoryName))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Restaurant>(true);
                }

                filter = filter.And(x => x.CategoryRestaurant.Name == restaurantFilter.CategoryName);
            }

            if (!string.IsNullOrWhiteSpace(restaurantFilter.Order))
            {
                switch (restaurantFilter.Order)
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

            if (restaurantFilter.Id != Guid.Empty)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Restaurant>(true);
                }

                filter = filter.And(x => x.Id == restaurantFilter.Id);
            }

            if (restaurantFilter.CategoryId != Guid.Empty)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Restaurant>(true);
                }

                filter = filter.And(x => x.CategoryRestaurant.Id == restaurantFilter.CategoryId);
            }

            if (restaurantFilter.CreatedAt.Year > 1)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Restaurant>(true);
                }

                filter = filter.And(x => x.Id == restaurantFilter.Id);
            }

            if (restaurantFilter.PageIndex != null)
            {
                if (restaurantFilter.PageIndex > 1)
                {
                    skip = restaurantFilter.PageIndex * 25;
                }
            }

            if (restaurantFilter.PageSize != null)
            {
                if (restaurantFilter.PageSize > 0)
                {
                    take = restaurantFilter.PageSize;
                }
            }

            return await _unitOfWork
                .RepositoryFactory
                .RestaurantRepository
                .Search(
                    filter,
                    ordeBy, 
                    skip, 
                    take);
        }

        public async Task<Restaurant> GetById(Guid id)
        {
            return await _unitOfWork
                .RepositoryFactory
                .RestaurantRepository
                .GetById(id);
        }

        public async Task<Restaurant> Update(Restaurant restaurant)
        {
            if (!Validate(new RestaurantValidation(), restaurant)) return null;

            var restaurantDB = await _unitOfWork
                .RepositoryFactory
                .RestaurantRepository
                .GetById(restaurant.Id);

            if (restaurantDB == null)
            {
                NotifyError("Not found");
                return await Task.FromResult<Restaurant>(null);
            }

            if (restaurant.CategoryRestaurant != null)
            {
                if (!Validate(new AddressRestaurantValidation(), restaurant.AddressRestaurant)) return null;

                var categoryRestaurant = await _unitOfWork
                .RepositoryFactory
                .CategoryRestaurantRepository
                .GetById(restaurant.IdCategoryRestaurant);

                if (categoryRestaurant == null)
                {
                    NotifyError("Not fround categoryRestaurant");
                    return await Task.FromResult<Restaurant>(null);
                }

            }

            if (restaurant.AddressRestaurant != null)
            {
                if (restaurantDB.AddressRestaurant.Street != restaurant.AddressRestaurant.Street)
                {
                    restaurantDB.AddressRestaurant.Street = restaurant.AddressRestaurant.Street;
                }

                if (restaurantDB.AddressRestaurant.District != restaurant.AddressRestaurant.District)
                {
                    restaurantDB.AddressRestaurant.District = restaurant.AddressRestaurant.District;
                }

                if (restaurantDB.AddressRestaurant.City != restaurant.AddressRestaurant.City)
                {
                    restaurantDB.AddressRestaurant.City = restaurant.AddressRestaurant.City;
                }

                if (restaurantDB.AddressRestaurant.County != restaurant.AddressRestaurant.County)
                {
                    restaurantDB.AddressRestaurant.County = restaurant.AddressRestaurant.County;
                }

                if (restaurantDB.AddressRestaurant.ZipCode != restaurant.AddressRestaurant.ZipCode)
                {
                    restaurantDB.AddressRestaurant.ZipCode = restaurant.AddressRestaurant.ZipCode;
                }

                if (restaurantDB.AddressRestaurant.Latitude != restaurant.AddressRestaurant.Latitude)
                {
                    restaurantDB.AddressRestaurant.Latitude = restaurant.AddressRestaurant.Latitude;
                }

                if (restaurantDB.AddressRestaurant.Longitude != restaurant.AddressRestaurant.Longitude)
                {
                    restaurantDB.AddressRestaurant.Longitude = restaurant.AddressRestaurant.Longitude;
                }
            }

            if (restaurantDB.Name != restaurant.Name)
            {
                restaurantDB.Name = restaurant.Name;
            }

            if (restaurantDB.Description != restaurant.Description)
            {
                restaurantDB.Description = restaurant.Description;
            }

            if (restaurantDB.Image != restaurant.Image)
            {
                restaurantDB.Image = restaurant.Image;
            }

            _unitOfWork
                .RepositoryFactory
                .RestaurantRepository
                .Update(restaurantDB);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(restaurantDB);
            }

            NotifyError("Error deleting entity");
            return await Task.FromResult<Restaurant>(null);
        }

        public async Task<Restaurant> Delete(Guid id)
        {
            var restaurant = await _unitOfWork
                .RepositoryFactory
                .RestaurantRepository
                .GetById(id);

            if (restaurant == null)
            {
                NotifyError("Not found");
                return await Task.FromResult<Restaurant>(null);
            }

            _unitOfWork
                .RepositoryFactory
                .RestaurantRepository
                .Remove(id);

            if (await _unitOfWork.Commit())
            {
                return await Task.FromResult(restaurant);
            }

            NotifyError("Error deleting entity");
            return await Task.FromResult<Restaurant>(null);
        }
    }
}
