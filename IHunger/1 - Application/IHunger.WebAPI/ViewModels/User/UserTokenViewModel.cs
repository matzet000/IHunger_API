using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IHunger.WebAPI.ViewModels.User
{
    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }

        public UserTokenViewModel()
        {

        }

        public UserTokenViewModel(Domain.Models.User user, IList<Claim> claims)
        {
            Id = user.Id.ToString();
            Email = user.Email;
            Claims = claims.Select(c => new ClaimViewModel(c));
        }

    }
}
