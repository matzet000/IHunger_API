using AutoMapper;
using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.WebAPI.Controllers;
using IHunger.WebAPI.ViewModels.CategoryProduct;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/category-product")]
    public class CategoryProductController : MainController
    {
        private readonly ICategoryProductService _categoryProductService;
        private readonly IMapper _mapper;

        public CategoryProductController(
            ICategoryProductService categoryProductService,
            IMapper mapper,
            INotifier notifier, 
            IUser appUser) : base(notifier, appUser)
        {
            _categoryProductService = categoryProductService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryProductViewModel>> Create(CategoryProductViewModel categoryProductViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var categoryProduct = await _categoryProductService.Create(_mapper.Map<CategoryProduct>(categoryProductViewModel));

            var resp = _mapper.Map<CategoryProductViewModel>(categoryProduct);

            return CustomResponse(resp);
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryProductViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<CategoryProductViewModel>>(await _categoryProductService.GetAll());
        }
    }
}
