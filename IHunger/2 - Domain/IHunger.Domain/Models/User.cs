using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IHunger.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Identity { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string Image { get; set; }

        #region EFCRelations
        public AddressUser AddressUser { get; set; }
        public IEnumerable<Order> Orders { get; set; }

        #endregion
    }
}
