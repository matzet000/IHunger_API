using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Infra.CrossCutting.ViewModels.User
{
    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public ProfileViewModel Profile { get; set; }
        public AddressViewModel Address { get; set; }

        public IEnumerable<ClaimViewModel> Claims { get; set; }

        public UserTokenViewModel()
        {

        }

        public UserTokenViewModel(string id, string email, IList<Claim> claims)
        {
            Id = id;
            Email = email;
            Claims = claims.Select(c => new ClaimViewModel(c));
        }

    }
}
