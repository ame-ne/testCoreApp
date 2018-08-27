using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testCoreApp.Models
{
    public static class IdentitySeedData 
    {
        private const string adminUserName = "Admin";
        private const string adminUserPassword = "1qaz!QAZ";
        private const string adminRoleName = "administrator";

        public static async Task EnsurePopulated(IServiceProvider services)
        {
            UserManager<AppUser> userManager = services.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            AppUser user = await userManager.FindByNameAsync(adminUserName);
            if (user == null)
            {
                user = new AppUser(adminUserName);
                await userManager.CreateAsync(user, adminUserPassword);
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                user.Email = "qweqwe@example.com";
                await userManager.UpdateAsync(user);
            }

            IdentityRole adminRole = await roleManager.FindByNameAsync(adminRoleName);
            if (adminRole == null)
            {
                adminRole = new IdentityRole(adminRoleName);
                await roleManager.CreateAsync(adminRole);
            }

            if (await userManager.IsInRoleAsync(user, adminRoleName) == false)
            {
                await userManager.AddToRoleAsync(user, adminRoleName);
            }

        }
    }
}
