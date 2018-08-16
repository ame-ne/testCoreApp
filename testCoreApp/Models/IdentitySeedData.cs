﻿using Microsoft.AspNetCore.Builder;
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
        private const string adminUser = "Admin";
        private const string adminPassword = "1qaz!QAZ";

        public static async Task EnsurePopulated(IServiceProvider services)
        {
            UserManager<IdentityUser> userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser(adminUser);
                await userManager.CreateAsync(user, adminPassword);
            }

        }
    }
}