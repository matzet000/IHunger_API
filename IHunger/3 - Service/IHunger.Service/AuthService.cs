using IHunger.Domain.Enumeration;
using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.Domain.Models.Validations;
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
            if (!Validate(new ProfileUserValidation(), user.ProfileUser)) return null;
            if (!Validate(new AddressUserValidation(), user.ProfileUser.AddressUser)) return null;

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

        private async Task<string> CreateSecurityToken(string email)
        {
            var user = await GetUser(email);

            if (user == null)
            {
                NotifyError("Incorrect username or password");
                return null;
            }

            var token = await _userManager.CreateSecurityTokenAsync(user);

            if (token != null)
            {
                return token.ToString();
            }

            return null;
        }

        private async Task<string> CreateEmailConfirmationToken(string email)
        {
            var user = await GetUser(email);

            if (user == null)
            {
                NotifyError("Incorrect username or password");
                return null;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            if (string.IsNullOrWhiteSpace(token))
            {
                NotifyError("Error generate token");
                return null;
            }

            return token;
        }

        private async Task<string> CreateEmailChangeToken(string email, string newEmail)
        {
            var user = await GetUser(email);

            if (user == null)
            {
                NotifyError("Incorrect username or password");
                return null;
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);

            if (string.IsNullOrWhiteSpace(token))
            {
                NotifyError("Error generate token");
                return null;
            }

            return token;
        }

        private async Task<bool> ConfirmEmail(string email, string token)
        {
            var user = await GetUser(email);

            if (user == null)
            {
                NotifyError("Incorrect username or password");
                return false;
            }

            var identityResp = await _userManager.ConfirmEmailAsync(user, token);

            if (identityResp.Succeeded)
            {
                return true;
            }

            return false;
        }

        private async Task<string> CreatePasswordResetToken(string email)
        {
            var user = await GetUser(email);

            if (user == null)
            {
                NotifyError("Incorrect username or password");
                return null;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (string.IsNullOrWhiteSpace(token))
            {
                NotifyError("Error generate token");
                return null;
            }

            return token;
        }

        private async Task<bool> RecoverPassword(string email, string password, string token)
        {
            var user = await GetUser(email);

            if (user == null)
            {
                NotifyError("Incorrect username or password");
                return false;
            }

            var identityResp = await _userManager.ChangePasswordAsync(user, password, token);


            if (identityResp.Succeeded)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> ChangeEmail(string oldEmail, string newEmail, string token)
        {
            var user = await GetUser(oldEmail);

            if (user == null)
            {
                NotifyError("Incorrect username or password");
                return false;
            }

            var identityResp = await _userManager.ChangeEmailAsync(user, newEmail, token);

            if (identityResp.Succeeded)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> ChangePassword(string email, string password, string newPassword)
        {
            var user = await GetUser(email);

            if (user == null)
            {
                NotifyError("Incorrect username or password");
                return false;
            }

            var identityResp = await _userManager.ChangePasswordAsync(user, password, newPassword);

            if (identityResp.Succeeded)
            {
                return true;
            }

            return false;
        }

        private async Task<User> GetUser(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        private async Task AddClaims(User user)
        {
            switch (user.ProfileUser.Type)
            {
                case (int) TypeProfile.Admin:
                    await AddClaimsAdmin(user);
                    break;
                case (int) TypeProfile.Client:
                    await AddClaimsClient(user);
                    break;
                case (int) TypeProfile.Restaurant:
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

            var userToken = new UserTokenViewModel(user.Id.ToString(), user.Email, claims);

            var profile = await _unitOfWork.RepositoryFactory.ProfileUserRepository.GetById(user.IdProfileUser);

            if (profile != null)
            {
                userToken.Profile = new ProfileViewModel();
                userToken.Profile.BirthDate = profile.BirthDate;
                userToken.Profile.Name = profile.Name;
                userToken.Profile.LastName = profile.LastName;

                switch (profile.Type)
                {
                    case 1:
                        userToken.Profile.Type = TypeProfile.Admin.ToString();
                        break;
                    case 2:
                        userToken.Profile.Type = TypeProfile.Client.ToString();

                        break;
                    case 3:
                        userToken.Profile.Type = TypeProfile.Restaurant.ToString();
                        break;
                    default:
                        throw new Exception("Access denied");
                }

                if(profile.AddressUser != null)
                {
                    userToken.Address = new AddressViewModel();
                    userToken.Address.Street = profile.AddressUser.Street;
                    userToken.Address.District = profile.AddressUser.District;
                    userToken.Address.County = profile.AddressUser.County;
                    userToken.Address.City = profile.AddressUser.City;
                    userToken.Address.ZipCode = profile.AddressUser.ZipCode;
                    userToken.Address.Latitude = profile.AddressUser.Latitude;
                    userToken.Address.Longitude = profile.AddressUser.Longitude;
                }    
            }
            
            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = userToken
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
           => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
