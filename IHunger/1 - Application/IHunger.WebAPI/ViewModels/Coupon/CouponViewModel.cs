using System;

namespace IHunger.WebAPI.ViewModels.Coupon
{
    public class CouponViewModel : BaseViewModel
    {
        public string Code { get; set; }
        public int Value { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
