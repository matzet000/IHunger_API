using IHunger.Domain.Models;
using IHunger.Infra.CrossCutting.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponseViewModel> GetJwt(string email);
        Task<LoginResponseViewModel> Register(User user, string password);
        Task<LoginResponseViewModel> Login(string email, string password);
    }
}
