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
        private const string workerRoleName = "worker";
        private const string userRoleName = "user";
        private static string[] appRoleNames = new string[] { adminRoleName, workerRoleName, userRoleName };

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

            await AddRolesAsync(roleManager);

            if (await userManager.IsInRoleAsync(user, adminRoleName) == false)
            {
                await userManager.AddToRoleAsync(user, adminRoleName);
            }

        }

        private static async Task AddRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach(var roleName in appRoleNames)
            {
                IdentityRole roleFromDB = await roleManager.FindByNameAsync(roleName);
                if (roleFromDB == null)
                {
                    roleFromDB = new IdentityRole(roleName);
                    await roleManager.CreateAsync(roleFromDB);
                }
            }
        }
    }
}
