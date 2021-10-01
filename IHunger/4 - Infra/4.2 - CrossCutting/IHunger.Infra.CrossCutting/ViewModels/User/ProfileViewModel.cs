using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Infra.CrossCutting.ViewModels.User
{
    public class ProfileViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Type { get; set; }
    }
}
