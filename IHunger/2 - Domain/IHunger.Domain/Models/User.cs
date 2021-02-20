using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Identity { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }

        #region EFCRelations
        public AddressUser AddressUser { get; set; }
        public IEnumerable<Order> Orders { get; set; }

        #endregion
    }
}
