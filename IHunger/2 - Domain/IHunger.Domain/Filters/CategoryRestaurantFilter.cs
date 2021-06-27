using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Filters
{
    public class CategoryRestaurantFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Order { get; set; }

        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
