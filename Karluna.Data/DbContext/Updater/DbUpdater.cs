using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karluna.Data.DbContext;
using Microsoft.Extensions.DependencyInjection;
using Karluna.Entities.Entities;
using Microsoft.AspNetCore.Identity;

namespace Karluna.Data.DbContext
{
    public static class DbUpdater
    {
        public static void UpdateDatabase(KtsDbContext context) 
        {
            try
            {
                var migrations = context.Database.GetPendingMigrations();
                if (migrations.Any())
                {
                    Console.WriteLine("Pending Migrations:");
                    foreach (var migration in migrations)
                    {
                        Console.WriteLine(migration);
                    }

                    Console.WriteLine("Applying Migrations...");
                    context.Database.Migrate();
                    Console.WriteLine("Migrations Applied Successfully!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static async Task InitializeSeed(UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            List<string> roles = new List<string>() 
            {
                "Karluna.Admin",
                "Stock.Manager",
                "Stock.User"
            };
            string adminRoleName = "Karluna.Admin";
            string userName = "karluna.admin";
            string password = "Karluna1234!";

            // Rollerin olup olmadığını kontrol et, yoksa oluştur.
            try
            {
                foreach (var roleName in roles)
                {
                    if (await roleManager.FindByNameAsync(roleName) == null)
                    {
                        await roleManager.CreateAsync(new UserRole(roleName));
                    }
                }
                

                // Admin kullanıcısının olup olmadığını kontrol et, yoksa oluştur.
                if (await userManager.FindByNameAsync(userName) == null)
                {
                    User user = new User()
                    {
                        UserName = userName,
                        Email = userName,
                    };

                    IdentityResult result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        // Admin kullanıcısını "Admin" rolüne atayın.
                        var result1 = await userManager.AddToRoleAsync(user, adminRoleName);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
