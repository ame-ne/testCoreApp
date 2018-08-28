using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testCoreApp.Components
{
    public class RolesCheckListViewComponent : ViewComponent
    {
        private RoleManager<IdentityRole> roleManager;

        public RolesCheckListViewComponent(RoleManager<IdentityRole> manager)
        {
            roleManager = manager;
        }

        public IViewComponentResult Invoke()
        {
            return View(roleManager.Roles.ToList());
        }
    }
}
