using AbdulAkinCengiz_222132128.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.DataAccess.Seeds;

public static class IdentitySeed
{
    public static async Task SeedAsync(IServiceProvider sp)
    {
        using var scope = sp.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        // 1) Roller
        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // 2) Admin kullanıcı
        var adminUserName = "admin";
        var adminEmail = "admin@local.test";
        var adminPassword = "Admin12345!"; // canlıda config/env ile yönet

        var admin = await userManager.FindByNameAsync(adminUserName);
        if (admin == null)
        {
            admin = new AppUser
            {
                UserName = adminUserName,
                Email = adminEmail,
                FirstName = "System",
                LastName = "Admin",
                EmailConfirmed = true,
                IsActive = true,
                IsDeleted = false
            };

            var createResult = await userManager.CreateAsync(admin, adminPassword);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, createResult.Errors.Select(e => $"{e.Code}: {e.Description}"));
                throw new Exception("Admin kullanıcı oluşturulamadı:\n" + errors);
            }
        }

        // 3) Role atama
        if (!await userManager.IsInRoleAsync(admin, "Admin"))
        {
            var roleResult = await userManager.AddToRoleAsync(admin, "Admin");
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, roleResult.Errors.Select(e => $"{e.Code}: {e.Description}"));
                throw new Exception("Admin role atanamadı:\n" + errors);
            }
        }
    }
}
