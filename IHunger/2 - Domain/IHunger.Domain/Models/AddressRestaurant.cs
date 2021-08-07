using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class AddressRestaurant : AddressBase
    {
        public AddressRestaurant()
        {

        }

        public Restaurant Restaurant
        {
            get => default;
            set
            {
            }
        }
    }
}
