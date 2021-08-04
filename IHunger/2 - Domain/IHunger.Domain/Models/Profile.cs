using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class ProfileUser : Entity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Type { get; set; }


        #region EFCRelations
        public Guid? IdAddressUser { get; set; }
        public virtual AddressUser AddressUser { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
        #endregion

        public ProfileUser()
        {

        }
    }
}
