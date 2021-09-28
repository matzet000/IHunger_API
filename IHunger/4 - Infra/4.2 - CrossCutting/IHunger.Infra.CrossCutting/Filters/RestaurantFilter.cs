using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.CrossCutting.Filters
{
    public class RestaurantFilter : BaseFilter
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

       
    }
}
