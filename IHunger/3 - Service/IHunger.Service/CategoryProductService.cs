using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Repository;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.Domain.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Service
{
    public class CategoryProductService : BaseService, ICategoryProductService
    {
        private readonly INotifier _notifier;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryProductService(INotifier notifier, IUnitOfWork unitOfWork) : base(notifier)
        {
            _notifier = notifier;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryProduct> Create(CategoryProduct categoryProduct)
        {
            if (!Validation(new CategoryProductValidation(), categoryProduct)) return null;

            var categoryProductBD =  await _unitOfWork
                .RepositoryFactory
                .CategoryProductRepository
                .Search(x => x.Name == categoryProduct.Name);

            if(categoryProductBD != null && categoryProductBD.Any())
            {
                NotifyError("Already exists categoryProduct the same name");
                return await Task.FromResult<CategoryProduct>(null);
            }

            await _unitOfWork.RepositoryFactory.CategoryProductRepository.Add(categoryProduct);

            if(await _unitOfWork.Commit())
            {
                return await Task.FromResult(categoryProduct);
            }

            NotifyError("Error inserting entity");
            return await Task.FromResult<CategoryProduct>(null);
        }

        public async Task<List<CategoryProduct>> GetAll()
        {
            return await _unitOfWork.RepositoryFactory.CategoryProductRepository.GetAll();
        }
    }
}
