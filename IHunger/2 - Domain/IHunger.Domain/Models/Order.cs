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
        public virtual IEnumerable<Item> Items { get; set; }
        
        public Guid? IdCoupon { get; set; }
        public virtual Coupon Coupon { get; set; }
        
        public Guid? IdProfileUser { get; set; }
        public virtual ProfileUser ProfileUser { get; set; }
        #endregion

        public Order()
        {

        }
    }
}
