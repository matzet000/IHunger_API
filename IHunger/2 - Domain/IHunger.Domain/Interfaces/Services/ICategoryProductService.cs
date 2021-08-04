using IHunger.Domain.Models;
using IHunger.Infra.CrossCutting.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHunger.Domain.Interfaces.Services
{
    public interface ICategoryProductService
    {
        Task<CategoryProduct> Create(CategoryProduct categoryProduct);
        Task<List<CategoryProduct>> GetAll();
        Task<List<CategoryProduct>> GetAllWithFilter(CategoryProductFilter categoryProductFilter);
        Task<CategoryProduct> GetById(Guid id);
        Task<CategoryProduct> Update(CategoryProduct categoryProduct);
        Task<CategoryProduct> Delete(Guid id);
    }
}
