using AutoMapper;
using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.WebAPI.Controllers;
using IHunger.WebAPI.Extensions;
using IHunger.WebAPI.ViewModels.Coupon;
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
    [Route("api/v{version:apiVersion}/coupons")]
    public class CouponController : MainController
    {
        private readonly ICouponService _couponService;
        private readonly IMapper _mapper;

        public CouponController(
            ICouponService couponService,
            IMapper mapper,
            INotifier notifier, 
            IUser appUser) : base(notifier, appUser)
        {
            _couponService = couponService;
            _mapper = mapper;
        }

        [HttpPost]
        [ClaimsAuthorize("CategoryProduct", "Create")]
        public async Task<ActionResult<CouponViewModel>> Create(CouponCreatedViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var entity = await _couponService.Create(_mapper.Map<Coupon>(viewModel));

            var resp = _mapper.Map<CouponViewModel>(entity);

            return CustomResponse(resp);
        }

        [HttpGet]
        [ClaimsAuthorize("CategoryProduct", "Get")]
        public async Task<IEnumerable<CouponViewModel>> GetAll([FromQuery] bool ative = true)
        {
            return _mapper.Map<IEnumerable<CouponViewModel>>(await _couponService.GetAll(ative));
        }

        [HttpGet("{id}")]
        [ClaimsAuthorize("CategoryProduct", "Get")]
        public async Task<CouponViewModel> GetById(Guid id)
        {
            return _mapper.Map<CouponViewModel>(await _couponService.GetById(id));
        }


        [HttpPut("{id}")]
        [ClaimsAuthorize("CategoryProduct", "Update")]
        public async Task<ActionResult<CouponViewModel>> Update([FromRoute] Guid id, [FromBody] CouponViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            if (id != viewModel.Id) return NotFound();

            var entity = await _couponService.Update(_mapper.Map<Coupon>(viewModel));

            var resp = _mapper.Map<CouponViewModel>(entity);

            return CustomResponse(resp);
        }

        [HttpDelete("{id}")]
        [ClaimsAuthorize("CategoryProduct", "Delete")]
        public async Task<CouponViewModel> Delete(Guid id)
        {
            return _mapper.Map<CouponViewModel>(await _couponService.Delete(id));
        }
    }
}
