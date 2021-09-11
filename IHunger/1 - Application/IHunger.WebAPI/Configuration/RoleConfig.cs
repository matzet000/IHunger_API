using IHunger.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.Configuration
{
    public class RoleConfig
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            // see later
            /*
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] rolesNames = { "Admin", "Client", "Restaurant" };
            List<Guid> rolesGuid = new List<Guid>();

            rolesGuid.Add(new Guid("D0DE1BA0-D68D-4F56-9160-8FDFFFF1C166")); // Admin
            rolesGuid.Add(new Guid("131569E2-C450-4576-A4EE-F015371CFFB8")); // Client
            rolesGuid.Add(new Guid("81C5A3C0-80E7-486B-AC1A-12719B023098")); // Restaurant

            IdentityResult result;
            foreach (var namesRole in rolesNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(namesRole);
                if (!roleExist)
                {
                    result = await roleManager.CreateAsync(new IdentityRole<Guid>(namesRole));
                }
            }
            */
        }
    }
}
