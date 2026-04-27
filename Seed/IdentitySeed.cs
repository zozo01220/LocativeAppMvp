using Microsoft.AspNetCore.Identity;

public static class IdentitySeed
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("SuperAdmin"))
            await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        //if (!await roleManager.RoleExistsAsync("User"))
        //    await roleManager.CreateAsync(new IdentityRole("User"));

        if (!await roleManager.RoleExistsAsync("Tenant"))
            await roleManager.CreateAsync(new IdentityRole("Tenant"));

        if (!await roleManager.RoleExistsAsync("Candidate"))
            await roleManager.CreateAsync(new IdentityRole("Candidate"));
    }
}