﻿using AutoMapper;
using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.Infra.CrossCutting.Filters;
using IHunger.WebAPI.Controllers;
using IHunger.WebAPI.ViewModels.Comment;
using IHunger.WebAPI.ViewModels.Restaurant;
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
    [Route("api/v{version:apiVersion}/restaurants")]
    public class RestaurantController : MainController
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public RestaurantController(
            IRestaurantService restaurantService,
            ICommentService commentService,
            IMapper mapper,
            INotifier notifier, 
            IUser appUser) : base(notifier, appUser)
        {
            _restaurantService = restaurantService;
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantViewModel>> Create(RestaurantCreatedViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var entity = await _restaurantService.Create(_mapper.Map<Restaurant>(viewModel));

            var resp = _mapper.Map<RestaurantViewModel>(entity);

            return CustomResponse(resp);
        }

        [HttpGet]
        public async Task<IEnumerable<RestaurantViewModel>> GetAllWithFilter([FromQuery] RestaurantFilter filter)
        {
            return _mapper.Map<IEnumerable<RestaurantViewModel>>(await _restaurantService.GetAllWithFilter(filter));
        }

        [HttpGet("{id}")]
        public async Task<RestaurantViewModel> GetById(Guid id)
        {
            return _mapper.Map<RestaurantViewModel>(await _restaurantService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RestaurantViewModel>> Update([FromRoute] Guid id, [FromBody] RestaurantViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            if (id != viewModel.Id) return NotFound();

            var entity = await _restaurantService.Update(_mapper.Map<Restaurant>(viewModel));

            var resp = _mapper.Map<RestaurantViewModel>(entity);

            return CustomResponse(resp);
        }

        [HttpDelete("{id}")]
        public async Task<RestaurantViewModel> Delete(Guid id)
        {
            return _mapper.Map<RestaurantViewModel>(await _restaurantService.Delete(id));
        }

        [HttpPost("{idRestaurant}/comments")]
        public async Task<ActionResult<CommentViewModel>> CreateComment([FromRoute] Guid idRestaurant, [FromBody] CommentCreatedViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var entity = await _commentService.Create(idRestaurant, _mapper.Map<Comment>(viewModel));

            var resp = _mapper.Map<CommentViewModel>(entity);

            return CustomResponse(resp);
        }

        [HttpGet("{idRestaurant}/comments")]
        public async Task<ActionResult<CommentViewModel>> GetAllComment([FromRoute] Guid idRestaurant)
        {
            return _mapper.Map<CommentViewModel>(await _commentService.GetAll(idRestaurant));
        }

        [HttpGet("{idRestaurant}/comments/{idComment}")]
        public async Task<ActionResult<CommentViewModel>> GetComment([FromRoute] Guid idRestaurant, [FromRoute] Guid idComment)
        {
            return _mapper.Map<CommentViewModel>(await _commentService.GetById(idRestaurant, idComment));
        }

        [HttpPut("{idRestaurant}/comments/{idComment}")]
        public async Task<ActionResult<CommentViewModel>> UpdateComment([FromRoute] Guid idRestaurant, [FromRoute] Guid idComment, [FromBody] CommentViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var entity = await _commentService.Update(idRestaurant, idComment, _mapper.Map<Comment>(viewModel));

            var resp = _mapper.Map<CommentViewModel>(entity);

            return CustomResponse(resp);
        }

        [HttpDelete("{idRestaurant}/comments/{idComment}")]
        public async Task<ActionResult<CommentViewModel>> DeleteComment([FromRoute] Guid idRestaurant, [FromRoute] Guid idComment)
        {
            return _mapper.Map<CommentViewModel>(await _commentService.Delete(idRestaurant, idComment));
        }
    }
}
