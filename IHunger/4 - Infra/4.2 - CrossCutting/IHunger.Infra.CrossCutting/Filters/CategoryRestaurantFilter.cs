using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.CrossCutting.Filters
{
    public class CategoryRestaurantFilter : BaseFilter
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
