using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class CategoryProduct : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        #region EFCRelations
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        #endregion
    }
}
