using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testCoreApp.Models;

namespace testCoreApp.Components
{
    public class RolesCheckListViewComponent : ViewComponent
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;

        public RolesCheckListViewComponent(RoleManager<IdentityRole> rManager, UserManager<AppUser> uManager)
        {
            roleManager = rManager;
            userManager = uManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(AppUser user)
        {
            Dictionary<string, bool> roles = new Dictionary<string, bool>();
            var allRoles = roleManager.Roles.ToList();
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                foreach (var role in allRoles)
                {
                    if (!roles.ContainsKey(role.Name))
                    {
                        roles.Add(role.Name, userRoles.Contains(role.Name));
                    }
                }
            }
            else
            {
                allRoles.ForEach(r => {
                    if (!roles.ContainsKey(r.Name))
                    {
                        roles.Add(r.Name, false);
                    }
                });
            }
            return View(roles);
        }
    }
}
