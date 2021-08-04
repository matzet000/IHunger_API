using IHunger.Domain.Models;
using IHunger.Infra.CrossCutting.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHunger.Domain.Interfaces.Services
{
    public interface ICategoryRestaurantService
    {
        Task<CategoryRestaurant> Create(CategoryRestaurant categoryRestaurant);
        Task<List<CategoryRestaurant>> GetAll();
        Task<List<CategoryRestaurant>> GetAllWithFilter(CategoryRestaurantFilter CategoryRestaurantFilter);
        Task<CategoryRestaurant> GetById(Guid id);
        Task<CategoryRestaurant> Update(CategoryRestaurant categoryRestaurant);
        Task<CategoryRestaurant> Delete(Guid id);
    }
}
