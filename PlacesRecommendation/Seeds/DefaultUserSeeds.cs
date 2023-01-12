using Microsoft.AspNetCore.Identity;
using PlacesRecommendation.Constant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlacesRecommendation.Seeds
{
    public static class DefaultUsersSeeds
    {
        public static async Task CreateDefaultUser(UserManager<IdentityUser> userManager)
        {
            IdentityUser defaultUser = new()
            {
                UserName = "BasicUser@Place.com",
                Email = "BasicUser@Place.com",
                EmailConfirmed = true,
            };
            IdentityUser user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "");
                await userManager.AddToRoleAsync(defaultUser, DefaultRoles.User.ToString());
            }
        }
        public static async Task CreateDefaultAdmin(UserManager<IdentityUser> userManager,
                                                     RoleManager<IdentityRole> roleManager)
        {
            IdentityUser defaultUser = new()
            {
                UserName = "SuperAdmin@Place.com",
                Email = "SuperAdmin@Place.com",
                EmailConfirmed = true,
            };
            IdentityUser user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "");
                await userManager.AddToRolesAsync(defaultUser, new List<string> {
                    DefaultRoles.SuperAdmin.ToString(),
                    DefaultRoles.User.ToString() ,
                    DefaultRoles.Admin.ToString() });
            }

        }
        
    }
}
