using IHunger.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class Order : Entity
    {
        public decimal Price { get; set; }
        public TypeOrderStatus OrderStatus { get; set; }

        #region EFCRelations
        public IEnumerable<Item> Items { get; set; }
        
        public Guid? CouponId { get; set; }
        public virtual Coupon Coupon { get; set; }
        
        public Guid? ProfileUserId { get; set; }
        public virtual ProfileUser ProfileUser { get; set; }
        #endregion
    }
}
