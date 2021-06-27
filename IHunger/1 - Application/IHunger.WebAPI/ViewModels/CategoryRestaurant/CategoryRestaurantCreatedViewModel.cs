using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.ViewModels.CategoryRestaurant
{
    public class CategoryRestaurantCreatedViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(50, ErrorMessage = "The field {0} need to have between {2} and {1} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(200, ErrorMessage = "The field {0} need to have between {2} and {1} characters", MinimumLength = 3)]
        public string Description { get; set; }
    }
}
