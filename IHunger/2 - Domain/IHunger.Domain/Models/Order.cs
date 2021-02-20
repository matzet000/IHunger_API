using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class Order : Entity
    {
        public double Price { get; set; }
        public TypeOrderStatus OrderStatus { get; set; }

        #region EFCRelations
        public IEnumerable<Item> Items { get; set; }
        public Coupon Coupon { get; set; }
        public Guid? CouponId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        #endregion
    }
}
