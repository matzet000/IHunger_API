using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Infra.CrossCutting.Filters
{
    public abstract class BaseFilter
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Order { get; set; }

        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
