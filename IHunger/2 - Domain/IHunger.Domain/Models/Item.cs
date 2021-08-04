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
        public Guid IdProduct { get; set; }
        public virtual Product Product { get; set; }
        public Guid IdOrder { get; set; }
        public virtual Order Order { get; set; }

        #endregion

        public Item()
        {

        }
    }
}
