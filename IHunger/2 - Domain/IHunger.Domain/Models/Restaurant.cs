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
        public IEnumerable<Product> Products { get; set; }
        public CategoryRestaurant CategoryRestaurant { get; set; }
        public AddressRestaurant AddressRestaurant { get; set; }

        #endregion
    }
}
