using IHunger.Domain.Enumeration;
using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.Infra.CrossCutting.Extensions;
using IHunger.Infra.CrossCutting.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Service
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly AppSettings _appSettings;

        public AuthService(
            INotifier notifier, 
            IUnitOfWork unitOfWork,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IOptions<AppSettings> appSettings) : 
            base(notifier, unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        public async Task<LoginResponseViewModel> Register(User user, string password)
        {
            var resultCreateUser = await _userManager.CreateAsync(user, password);

            if (resultCreateUser.Succeeded)
            {
                await AddClaims(user);
                await _signInManager.SignInAsync(user, false);
                return await GetJwt(user.Email);
            }

            foreach (var error in resultCreateUser.Errors)
            {
                NotifyError(error.Description);
            }

            return await GetJwt(user.Email);
        }

        public async Task<LoginResponseViewModel> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (result.Succeeded)
            {
                return await GetJwt(email);
            }
            else if (result.IsLockedOut)
            {
                NotifyError("User temporarily blocked");
                return null;
            }

            NotifyError("Incorrect username or password");
            return null;
        }

        private async Task AddClaims(User user)
        {
            switch (user.ProfileUser.Type)
            {
                case (int) TypeProfile.Admin:
                    await AddClaimsAdmin(user);
                    break;
                case (int)TypeProfile.Client:
                    await AddClaimsClient(user);
                    break;
                case (int)TypeProfile.Restaurant:
                    await AddClaimsRestaurant(user);
                    break;
            }
        }

        private async Task AddClaimsAdmin(User user)
        {
            //await _userManager.AddToRoleAsync(user, "Admin");

            await _userManager.AddClaimAsync(user, new Claim("Type", "Admin"));

            await _userManager.AddClaimAsync(user, new Claim("CategoryProduct", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("CategoryProduct", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("CategoryProduct", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("CategoryProduct", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("CategoryRestaurant", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("CategoryRestaurant", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("CategoryRestaurant", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("CategoryRestaurant", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("Coupon", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("Coupon", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Coupon", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("Coupon", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("Item", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("Item", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Item", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("Item", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("Order", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("Order", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Order", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("Order", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("OrderStatus", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("OrderStatus", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("OrderStatus", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("OrderStatus", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("Product", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("Product", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Product", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("Product", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("Profile", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("Profile", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Profile", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("Profile", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("Restaurant", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("Restaurant", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Restaurant", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("Restaurant", "Delete"));
        }

        private async Task AddClaimsClient(User user)
        {
            //await _userManager.AddToRoleAsync(user, "Client");

            await _userManager.AddClaimAsync(user, new Claim("Type", "Client"));

            await _userManager.AddClaimAsync(user, new Claim("CategoryProduct", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("CategoryRestaurant", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Coupon", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Product", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("OrderStatus", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Restaurant", "Get"));

            await _userManager.AddClaimAsync(user, new Claim("Item", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("Item", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Item", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("Item", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("Order", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("Order", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Order", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("Order", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("Profile", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Profile", "Update"));
        }

        private async Task AddClaimsRestaurant(User user)
        {
            //await _userManager.AddToRoleAsync(user, "Restaurant");

            await _userManager.AddClaimAsync(user, new Claim("Type", "Restaurant"));

            await _userManager.AddClaimAsync(user, new Claim("Product", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("Product", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Product", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("Product", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("Coupon", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("Coupon", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Coupon", "Update"));

            await _userManager.AddClaimAsync(user, new Claim("Restaurant", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("Restaurant", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Restaurant", "Update"));

            await _userManager.AddClaimAsync(user, new Claim("Profile", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Profile", "Update"));

            await _userManager.AddClaimAsync(user, new Claim("OrderStatus", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("OrderStatus", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("OrderStatus", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("OrderStatus", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("Order", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("Order", "Update"));
            await _userManager.AddClaimAsync(user, new Claim("Order", "Delete"));

            await _userManager.AddClaimAsync(user, new Claim("CategoryRestaurant", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("CategoryRestaurant", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("CategoryRestaurant", "Update"));

            await _userManager.AddClaimAsync(user, new Claim("CategoryProduct", "Create"));
            await _userManager.AddClaimAsync(user, new Claim("CategoryProduct", "Get"));
            await _userManager.AddClaimAsync(user, new Claim("CategoryProduct", "Update"));
        }


        public async Task<LoginResponseViewModel> GetJwt(string email)
        {
           var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidOn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserTokenViewModel(user.Id.ToString(), user.Email, claims)
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
           => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
