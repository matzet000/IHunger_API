using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class AddressUser : AddressBase
    {
        #region EFCRelations
        public Guid UserId { get; set; }
        public User User { get; set; }

        #endregion
    }
}
