using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Repository;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.Domain.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Service
{
    public class CategoryProductService : BaseService, ICategoryProductService
    {
        private readonly INotifier _notifier;
        private readonly ICategoryProductRepository _categoryProductRepository;

        public CategoryProductService(INotifier notifier, ICategoryProductRepository categoryProductRepository) : base(notifier)
        {
            _notifier = notifier;
            _categoryProductRepository = categoryProductRepository;
        }

        public async Task<CategoryProduct> Create(CategoryProduct categoryProduct)
        {
            if (!Validation(new CategoryProductValidation(), categoryProduct)) return null;

            await _categoryProductRepository.Add(categoryProduct);

            return await Task.FromResult(categoryProduct);
        }
    }
}
