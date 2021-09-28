using IHunger.WebAPI.ViewModels.Address;
using IHunger.WebAPI.ViewModels.CategoryRestaurant;
using IHunger.WebAPI.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.ViewModels.Restaurant
{
    public class RestaurantViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public CategoryRestaurantViewModel CategoryRestaurant { get; set; }
        public AddressRestaurantViewModel AddressRestaurant { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
