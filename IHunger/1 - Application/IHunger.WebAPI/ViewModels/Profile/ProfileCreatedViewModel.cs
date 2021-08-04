using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.ViewModels.Profile
{
    public class ProfileCreatedViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Type { get; set; }
    }
}
