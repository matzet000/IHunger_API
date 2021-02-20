using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class Item : Entity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        #region EFCRelations
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        #endregion
    }
}
