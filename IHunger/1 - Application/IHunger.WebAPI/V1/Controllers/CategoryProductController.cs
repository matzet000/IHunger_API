using AutoMapper;
using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.Infra.CrossCutting.Filters;
using IHunger.WebAPI.Controllers;
using IHunger.WebAPI.Extensions;
using IHunger.WebAPI.ViewModels.CategoryProduct;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHunger.WebAPI.V1.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/category-products")]
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
        [ClaimsAuthorize("CategoryProduct", "Create")]
        public async Task<ActionResult<CategoryProductViewModel>> Create(CategoryProductCreatedViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var categoryProduct = await _categoryProductService.Create(_mapper.Map<CategoryProduct>(viewModel));

            var resp = _mapper.Map<CategoryProductViewModel>(categoryProduct);

            return CustomResponse(resp);
        }

        [HttpGet]
        [ClaimsAuthorize("CategoryProduct", "Get")]
        public async Task<IEnumerable<CategoryProductViewModel>> GetAllWithFilter([FromQuery] CategoryProductFilter filter)
        {
            return _mapper.Map<IEnumerable<CategoryProductViewModel>>(await _categoryProductService.GetAllWithFilter(filter));
        }

        [HttpGet("{id}")]
        [ClaimsAuthorize("CategoryProduct", "Get")]
        public async Task<CategoryProductViewModel> GetById(Guid id)
        {
            return _mapper.Map<CategoryProductViewModel>(await _categoryProductService.GetById(id));
        }

        [HttpPut("{id}")]
        [ClaimsAuthorize("CategoryProduct", "Update")]
        public async Task<ActionResult<CategoryProductViewModel>> Update([FromRoute] Guid id, [FromBody] CategoryProductViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            if (id != viewModel.Id) return NotFound();

            var categoryProduct = await _categoryProductService.Update(_mapper.Map<CategoryProduct>(viewModel));

            var resp = _mapper.Map<CategoryProductViewModel>(categoryProduct);

            return CustomResponse(resp);
        }

        [HttpDelete("{id}")]
        [ClaimsAuthorize("CategoryProduct", "Delete")]
        public async Task<CategoryProductViewModel> Delete(Guid id)
        {
            return _mapper.Map<CategoryProductViewModel>(await _categoryProductService.Delete(id));
        }
    }
}
