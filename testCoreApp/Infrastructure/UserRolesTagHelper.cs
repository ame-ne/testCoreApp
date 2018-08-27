using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testCoreApp.Models;

namespace testCoreApp.Infrastructure
{
    [HtmlTargetElement("span", Attributes = "user-id")]
    public class UserRolesTagHelper : TagHelper
    {
        private UserManager<AppUser> userManager;

        public UserRolesTagHelper(UserManager<AppUser> manager)
        {
            userManager = manager;
        }

        public string UserId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                var roles = await userManager.GetRolesAsync(user);
                output.Content.Append(string.Join(", ", roles));
            }
        }

    }
}
