using Ammeg.Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ammeg.Blog.Data
{
    public static class SeedData
    {


        public static async Task Migrations(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    await dbContext.Database.MigrateAsync();
                }
            }
        }

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            string[] Roles = new string[] { "Administradores" };

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userRole = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var user = new ApplicationUser() { Email = "admin@blog.com.br", UserName = "admin@blog.com.br", EmailConfirmed = true };

            if (await userManager.FindByEmailAsync(user.Email) == null)
            {
                var userResult = await userManager.CreateAsync(user, "Admin123@");

                if (userResult.Succeeded)
                {
                    user = await userManager.FindByEmailAsync(user.Email);
                    await userManager.AddToRoleAsync(user, "Administradores");
                }
            }
        }
    }
}