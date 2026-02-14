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

            //security setup 
            const string ADMIN_ID = "c719cdfc-46bd-42ab-92af-e27ac10233d5";
            const string MANAGER_ID = "565684fa-fad1-402a-81ed-c9a3a8d2b9b7";
            const string USER_ID = "87a304f7-786a-43f4-a29f-809ab39322c1";
            const string OWNER_ID = "d6b28694-ccb6-42ba-8b11-800721f69b04";


            const string Administrator_Role_Id = "b4a2a63e-7bf8-4ce7-b193-0e47929b1916";
            const string Manager_Role_Id = "6d44c0db-a425-43fd-b768-06387bec830e";
            const string User_Role_Id = "369c0e6b-b296-4db8-a020-a6d49cc13887";
            const string Owner_Role_Id = "0c4c8ff5-91c5-4871-a179-509642862ab3";


            

        }

        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Define roles to seed
            var roles = new[] { "Admin", "Driver", "Customer" };

            // Seed roles
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Define the admin user details
            var adminEmail = "admin@gmail.com";
            var adminPassword = "Admin@123";

            // Check if the admin user already exists
            var userExist = await userManager.FindByEmailAsync(adminEmail);
            if (userExist == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    FirstName = "Admin",
                    PhoneNumber = "0712345678",
                    EmailConfirmed = true
                };

                // Create the admin user
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    // Assign the Admin role to the user
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception("Failed to create the admin user: " + string.Join(", ", result.Errors));
                }
            }
        }
    }
}
