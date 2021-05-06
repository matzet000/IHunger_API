using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class Restaurant : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        #region EFCRelations
        public virtual IEnumerable<Product> Products { get; set; }
        public Guid CategoryRestaurantId { get; set; }
        public virtual CategoryRestaurant CategoryRestaurant { get; set; }
        public Guid AddressRestaurantId { get; set; }
        public virtual AddressRestaurant AddressRestaurant { get; set; }

        #endregion
    }
}
