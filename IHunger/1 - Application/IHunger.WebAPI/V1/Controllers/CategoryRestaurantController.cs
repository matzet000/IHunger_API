using AutoMapper;
using IHunger.Domain.Filters;
using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.WebAPI.Controllers;
using IHunger.WebAPI.ViewModels.CategoryRestaurant;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/category-restaurants")]
    public class CategoryRestaurantController : MainController
    {
        private readonly ICategoryRestaurantService _categoryRestaurantService;
        private readonly IMapper _mapper;

        public CategoryRestaurantController(
            ICategoryRestaurantService categoryRestaurantService,
            IMapper mapper,
            INotifier notifier,
            IUser appUser) : base(notifier, appUser)
        {
            _categoryRestaurantService = categoryRestaurantService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryRestaurantViewModel>> Create(CategoryRestaurantCreatedViewModel categoryProductViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var categoryProduct = await _categoryRestaurantService.Create(_mapper.Map<CategoryRestaurant>(categoryProductViewModel));

            var resp = _mapper.Map<CategoryRestaurantViewModel>(categoryProduct);

            return CustomResponse(resp);
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryRestaurantViewModel>> GetAllWithFilter([FromQuery] CategoryRestaurantFilter categoryProductFilter)
        {
            return _mapper.Map<IEnumerable<CategoryRestaurantViewModel>>(await _categoryRestaurantService.GetAllWithFilter(categoryProductFilter));
        }

        [HttpGet("{id}")]
        public async Task<CategoryRestaurantViewModel> GetById(Guid id)
        {
            return _mapper.Map<CategoryRestaurantViewModel>(await _categoryRestaurantService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryRestaurantViewModel>> Update([FromRoute] Guid id, [FromBody] CategoryRestaurantViewModel categoryProductViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            if (id != categoryProductViewModel.Id) return NotFound();

            var categoryProduct = await _categoryRestaurantService.Update(_mapper.Map<CategoryRestaurant>(categoryProductViewModel));

            var resp = _mapper.Map<CategoryRestaurantViewModel>(categoryProduct);

            return CustomResponse(resp);
        }

        [HttpDelete("{id}")]
        public async Task<CategoryRestaurantViewModel> Delete(Guid id)
        {
            return _mapper.Map<CategoryRestaurantViewModel>(await _categoryRestaurantService.Delete(id));
        }
    }
}
