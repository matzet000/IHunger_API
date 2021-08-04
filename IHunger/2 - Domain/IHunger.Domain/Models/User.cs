using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IHunger.Domain.Models
{
    public class User : IdentityUser<Guid>
    {

        #region EFCRelations
        public Guid IdProfileUser { get; set; }
        public virtual ProfileUser ProfileUser { get; set; }
        #endregion

        public User()
        {

        }
    }
}
