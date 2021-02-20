using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class CategoryRestaurant : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        #region EFCRelations
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        #endregion
    }
}
