using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class Coupon : Entity
    {
        public string Code { get; set; }
        public int Value { get; set; }
        public DateTime ExpireAt { get; set; }

        #region EFCRelations
        public virtual IEnumerable<Order> Orders { get; set; }

        #endregion

        public Coupon()
        {

        }
    }
}
