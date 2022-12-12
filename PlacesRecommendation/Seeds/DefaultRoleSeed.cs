using Microsoft.AspNetCore.Identity;
using PlacesRecommendation.Constant;
using System.Linq;
using System.Threading.Tasks;

namespace PlacesRecommendation.Seeds
{
    public static class DefaultRoleSeed
    {
        public static async Task SeedDefaultRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(DefaultRoles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(DefaultRoles.User.ToString()));
                await roleManager.CreateAsync(new IdentityRole(DefaultRoles.SuperAdmin.ToString()));
            }

        }
    }
}
