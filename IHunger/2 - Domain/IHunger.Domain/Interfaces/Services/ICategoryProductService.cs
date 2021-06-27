using IHunger.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Domain.Interfaces.Services
{
    public interface ICategoryProductService
    {
        Task<CategoryProduct> Create(CategoryProduct categoryProduct);
        Task<List<CategoryProduct>> GetAll();
    }
}
