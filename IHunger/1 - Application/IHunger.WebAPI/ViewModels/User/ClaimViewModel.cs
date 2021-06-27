using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IHunger.WebAPI.ViewModels.User
{
    public class ClaimViewModel
    {
        public string Value { get; set; }
        public string Type { get; set; }

        public ClaimViewModel()
        {

        }

        public ClaimViewModel(Claim claim)
        {
            Value = claim.Value;
            Type = claim.Type;
        }
    }
}
