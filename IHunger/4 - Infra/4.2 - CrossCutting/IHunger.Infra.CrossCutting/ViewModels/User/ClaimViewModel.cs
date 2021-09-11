using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Infra.CrossCutting.ViewModels.User
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
