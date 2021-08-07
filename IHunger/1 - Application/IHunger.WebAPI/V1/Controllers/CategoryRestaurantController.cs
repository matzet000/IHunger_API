using AutoMapper;
using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.Infra.CrossCutting.Filters;
using IHunger.WebAPI.Controllers;
using IHunger.WebAPI.ViewModels.CategoryRestaurant;
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
        public async Task<ActionResult<CategoryRestaurantViewModel>> Create(CategoryRestaurantCreatedViewModel categoryRestaurantViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var categoryRestaurant = await _categoryRestaurantService.Create(_mapper.Map<CategoryRestaurant>(categoryRestaurantViewModel));

            var resp = _mapper.Map<CategoryRestaurantViewModel>(categoryRestaurant);

            return CustomResponse(resp);
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryRestaurantViewModel>> GetAllWithFilter([FromQuery] CategoryRestaurantFilter categoryRestaurantFilter)
        {
            return _mapper.Map<IEnumerable<CategoryRestaurantViewModel>>(await _categoryRestaurantService.GetAllWithFilter(categoryRestaurantFilter));
        }

        [HttpGet("{id}")]
        public async Task<CategoryRestaurantViewModel> GetById(Guid id)
        {
            return _mapper.Map<CategoryRestaurantViewModel>(await _categoryRestaurantService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryRestaurantViewModel>> Update([FromRoute] Guid id, [FromBody] CategoryRestaurantViewModel categoryRestaurantViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            if (id != categoryRestaurantViewModel.Id) return NotFound();

            var categoryRestaurant = await _categoryRestaurantService.Update(_mapper.Map<CategoryRestaurant>(categoryRestaurantViewModel));

            var resp = _mapper.Map<CategoryRestaurantViewModel>(categoryRestaurant);

            return CustomResponse(resp);
        }

        [HttpDelete("{id}")]
        public async Task<CategoryRestaurantViewModel> Delete(Guid id)
        {
            return _mapper.Map<CategoryRestaurantViewModel>(await _categoryRestaurantService.Delete(id));
        }
    }
}
