using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Boolean Vegan { get; set; }
        public Boolean Vegetarian { get; set; }
        public Boolean Kosher { get; set; }
        public string Image { get; set; }

        #region EFCRelations
        public Guid IdCategoryProduct { get; set; }
        public virtual CategoryProduct CategoryProduct { get; set; }
        public Guid IdRestaurant { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual IEnumerable<Item> Itens { get; set; }

        #endregion

        public Product()
        {

        }
    }
}
