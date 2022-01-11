using IHunger.Domain.Models;
using IHunger.Infra.CrossCutting.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<Product> Create(Product product);
        Task<List<Product>> GetAllWithFilter(ProductFilter productFilter);
        Task<Product> GetById(Guid id);
        Task<Product> GetByRestaurantByIdProduct(Guid idRestaurant, Guid idProduct);
        Task<List<Product>> GetByRestaurant(Guid id);
        Task<Product> Update(Product product);
        Task<Product> Delete(Guid id);
    }
}
