using IHunger.Domain.Models;
using IHunger.Infra.CrossCutting.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHunger.Domain.Interfaces.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant> Create(Restaurant restaurant);
        Task<List<Restaurant>> GetAllWithFilter(RestaurantFilter restaurantFilter);
        Task<Restaurant> GetById(Guid id);
        Task<Restaurant> Update(Restaurant restaurant);
        Task<Restaurant> Delete(Guid id);
    }
}
