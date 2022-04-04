using IHunger.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Domain.Interfaces.Services
{
    public interface ICouponService
    {
        Task<Coupon> Create(Coupon coupon);
        Task<List<Coupon>> GetAll(bool ative = true);
        Task<Coupon> GetById(Guid id);
        Task<Coupon> Update(Coupon coupon);
        Task<Coupon> Delete(Guid id);
    }
}
