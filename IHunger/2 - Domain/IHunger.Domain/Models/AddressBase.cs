using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public abstract class AddressBase : Entity
    {
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
