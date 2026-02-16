using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Net.NetworkInformation;
using Web.Data.Models;

namespace Web.Data
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            //sales group setup - this is required for the application to work, so we will add it if it doesn't exist.
            if (!context.SalesGroups.Where(x => x.Id == 1).Any())
            {

                context.SalesGroups.Add(new _SalesGroup()
                {
                    Id = 1,
                    SalesGroupName = "Default Name",
                    SalesGroupEmail = "",
                    CreatedDate = DateTime.Now
                });

                var thisTrans = context.Database.BeginTransaction();

                context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.SalesGroups ON;");
                context.SaveChanges();
                context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.SalesGroups OFF;");

                thisTrans.Commit();
            }
        }

        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Define roles to seed
            var roles = new[] { "Administrator", "Owner", "Manager", "User" };

            // Seed roles
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Define the admin user detail
            var testPassword = "Test123!";

            // Check if the admin user already exists
            var userExist = await userManager.FindByEmailAsync("admin@d.com");
            if (userExist == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@d.com",
                    Email = "admin@d.com",
                    SalesGroupID =1, // the testing sales group 
                    EmailConfirmed = true
                };

                // Create the admin user
                var result = await userManager.CreateAsync(adminUser, testPassword);
                if (result.Succeeded)
                {
                    // Assign the Admin role to the user
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                }
                else
                {
                    throw new Exception("Failed to create the admin user: " + string.Join(", ", result.Errors));
                }
            }

            // Check if the owner user already exists
            userExist = await userManager.FindByEmailAsync("owner@d.com");
            if (userExist == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "owner@d.com",
                    Email = "owner@d.com",
                    SalesGroupID = 1, // the testing sales group 
                    EmailConfirmed = true
                };

                // Create the admin user
                var result = await userManager.CreateAsync(adminUser, testPassword);
                if (result.Succeeded)
                {
                    // Assign the Admin role to the user
                    await userManager.AddToRoleAsync(adminUser, "Owner");
                }
                else
                {
                    throw new Exception("Failed to create the owner user: " + string.Join(", ", result.Errors));
                }
            }

            // Check if the manager user already exists
            userExist = await userManager.FindByEmailAsync("manager@d.com");
            if (userExist == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "manager@d.com",
                    Email = "manager@d.com",
                    SalesGroupID = 1, // the testing sales group 
                    EmailConfirmed = true
                };

                // Create the admin user
                var result = await userManager.CreateAsync(adminUser, testPassword);
                if (result.Succeeded)
                {
                    // Assign the Admin role to the user
                    await userManager.AddToRoleAsync(adminUser, "Manager");
                }
                else
                {
                    throw new Exception("Failed to create the manager user: " + string.Join(", ", result.Errors));
                }
            }

            // Check if the user user already exists
            userExist = await userManager.FindByEmailAsync("user@d.com");
            if (userExist == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "user@d.com",
                    Email = "user@d.com",
                    SalesGroupID = 1, // the testing sales group 
                    EmailConfirmed = true
                };

                // Create the admin user
                var result = await userManager.CreateAsync(adminUser, testPassword);
                if (result.Succeeded)
                {
                    // Assign the Admin role to the user
                    await userManager.AddToRoleAsync(adminUser, "User");
                }
                else
                {
                    throw new Exception("Failed to create the owner user: " + string.Join(", ", result.Errors));
                }
            }
        }
    }
}
