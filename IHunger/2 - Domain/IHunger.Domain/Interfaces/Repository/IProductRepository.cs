using IHunger.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Domain.Interfaces.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetByRestaurant(Guid id);
        Task<Product> GetByRestaurantByIdProduct(Guid idRestaurant, Guid idProduct);
    }
}
