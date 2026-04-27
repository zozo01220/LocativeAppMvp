using LocativeApp.Data;
using Microsoft.AspNetCore.Identity;

public static class SuperAdminSeed
{
    public static async Task CreateSuperAdmin(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        var email = "admin@system.local";

        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                OwnerId = null, // 🔥 IMPORTANT SaaS
                RoleType = "SuperAdmin"
            };

            await userManager.CreateAsync(user, "Admin@123!");
            await userManager.AddToRoleAsync(user, "SuperAdmin");
        }
    }
}