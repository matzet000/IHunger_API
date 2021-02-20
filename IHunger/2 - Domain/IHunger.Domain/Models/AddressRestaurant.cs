using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class AddressRestaurant : AddressBase
    {

        #region EFCRelations
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        #endregion
    }
}
