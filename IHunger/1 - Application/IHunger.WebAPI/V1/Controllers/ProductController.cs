using AutoMapper;
using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.Infra.CrossCutting.Filters;
using IHunger.WebAPI.Controllers;
using IHunger.WebAPI.Extensions;
using IHunger.WebAPI.ViewModels.Product;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.V1.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductController : MainController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(
            IProductService productService,
            IMapper mapper,
            INotifier notifier, 
            IUser appUser) : base(notifier, appUser)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        [ClaimsAuthorize("Product", "Create")]
        public async Task<ActionResult<ProductViewModel>> Create(ProductCreatedViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var entity = await _productService.Create(_mapper.Map<Product>(viewModel));

            var resp = _mapper.Map<ProductViewModel>(entity);

            return CustomResponse(resp);
        }

        [HttpGet]
        [ClaimsAuthorize("Product", "Get")]
        public async Task<IEnumerable<ProductViewModel>> GetAllWithFilter([FromQuery] ProductFilter filter)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productService.GetAllWithFilter(filter));
        }

        [HttpGet("{id}")]
        [ClaimsAuthorize("Product", "Get")]
        public async Task<ProductViewModel> GetById(Guid id)
        {
            var entity = await _productService.GetById(id);

            return _mapper.Map<ProductViewModel>(entity);
        }

        [HttpDelete("{id}")]
        [ClaimsAuthorize("Product", "Delete")]
        public async Task<ProductViewModel> Delete(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productService.Delete(id));
        }
    }
}
