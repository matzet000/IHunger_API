using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.ViewModels.Product
{
    public class ProductCreatedViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Boolean Vegan { get; set; }
        public Boolean Vegetarian { get; set; }
        public Boolean Kosher { get; set; }
        public string Image { get; set; }

        public Guid IdCategoryProduct { get; set; }

        public Guid IdRestaurant { get; set; }
    }
}
