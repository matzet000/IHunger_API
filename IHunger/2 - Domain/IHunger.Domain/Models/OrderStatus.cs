using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class OrderStatus : Entity
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public OrderStatus()
        {

        }
    }
}
