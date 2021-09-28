using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.CrossCutting.Filters
{
    public class CategoryProductFilter : BaseFilter
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
