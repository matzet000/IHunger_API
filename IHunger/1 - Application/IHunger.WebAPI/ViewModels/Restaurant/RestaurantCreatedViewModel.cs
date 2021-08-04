using IHunger.WebAPI.ViewModels.Address;
using IHunger.WebAPI.ViewModels.CategoryRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.ViewModels.Restaurant
{
    public class RestaurantCreatedViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid IdCategoryRestaurant { get; set; }
        public AddressRestaurantCreatedViewModel Address { get; set; }
    }
}
