using System;

namespace IHunger.WebAPI.ViewModels.Coupon
{
    public class CouponCreatedViewModel
    {
        public string Code { get; set; }
        public int Value { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
