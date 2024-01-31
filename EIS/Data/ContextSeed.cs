using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIS.Models;
using Microsoft.IdentityModel.Tokens;

namespace EIS.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            if ((await roleManager.FindByNameAsync("SuperAdmin")) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.SuperAdmin.ToString()));
            }
            if ((await roleManager.FindByNameAsync("Admin")) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            }
            if ((await roleManager.FindByNameAsync("Moderator")) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Moderator.ToString()));
            }
            if ((await roleManager.FindByNameAsync("Basic")) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Basic.ToString()));
            }
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "attila.palfi",
                Email = "attila.palfi@hu.eckerle-gruppe.com",
                FirstName = "Attila",
                LastName = "Palfi",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "EckerleHU1!");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.SuperAdmin.ToString());
                }

            }
        }
    }
}
