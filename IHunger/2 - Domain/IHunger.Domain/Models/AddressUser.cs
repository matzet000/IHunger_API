using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class AddressUser : AddressBase
    {
        #region EFCRelations
        public Guid? ProfileUserId { get; set; }
        public virtual ProfileUser ProfileUser { get; set; }

        #endregion
    }
}
