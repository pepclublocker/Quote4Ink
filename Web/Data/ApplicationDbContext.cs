using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Web.Data.Models;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<_SalesGroup> SalesGroups { get; set; }
        public DbSet<_Company> Companies { get; set; }






//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            Seed(modelBuilder); //seed our data for testing
//        //    SeedSecurity(modelBuilder); //seed our security for testing

//            base.OnModelCreating(modelBuilder);
//        }

//        private void Seed(ModelBuilder builder)
//        {
//            builder.Entity<_SalesGroup>().HasData(new _SalesGroup()
//            {
//                Id = 1,
//                SalesGroupName = "Default Name",
//                SalesGroupEmail = "",
//                CreatedDate = new DateTime(2024, 6, 1)
//            });
//        }

//        private void SeedSecurity(ModelBuilder builder)
//        {
//            const string ADMIN_ID = "c719cdfc-46bd-42ab-92af-e27ac10233d5";
//            const string MANAGER_ID = "565684fa-fad1-402a-81ed-c9a3a8d2b9b7";
//            const string USER_ID = "87a304f7-786a-43f4-a29f-809ab39322c1";
//            const string OWNER_ID = "d6b28694-ccb6-42ba-8b11-800721f69b04";


//            const string Administrator_Role_Id = "b4a2a63e-7bf8-4ce7-b193-0e47929b1916";
//            const string Manager_Role_Id = "6d44c0db-a425-43fd-b768-06387bec830e";
//            const string User_Role_Id = "369c0e6b-b296-4db8-a020-a6d49cc13887";
//            const string Owner_Role_Id = "0c4c8ff5-91c5-4871-a179-509642862ab3";

//            builder.Entity<IdentityRole>().HasData(new IdentityRole
//            {
//                Id = Administrator_Role_Id,
//                Name = "Administrator",
//                NormalizedName = "Administrator"
//            });

//            builder.Entity<IdentityRole>().HasData(new IdentityRole
//            {
//                Id = Owner_Role_Id,
//                Name = "Owner",
//                NormalizedName = "Owner"
//            });

//            builder.Entity<IdentityRole>().HasData(new IdentityRole
//            {
//                Id = Manager_Role_Id,
//                Name = "Manager",
//                NormalizedName = "Manager"
//            });

//            builder.Entity<IdentityRole>().HasData(new IdentityRole
//            {
//                Id = User_Role_Id,
//                Name = "User",
//                NormalizedName = "User"
//            });

//#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type. 

//            //Null in hasher.HashPassword is lazy but valid - Dylan

//            var hasher = new PasswordHasher<ApplicationUser>();
//            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
//            {
//                Id = ADMIN_ID,
//                Email = "admin@d.com",
//                NormalizedEmail = "ADMIN@D.COM",
//                UserName = "Admin@d.com",
//                NormalizedUserName = "ADMIN@D.COM",
//                EmailConfirmed = false,
//                PasswordHash = hasher.HashPassword(null, "Test123!"),
//                SecurityStamp = "8542d5f8-c7bb-42a4-9db9-f350165b96bc",
//                SalesGroupID = 1
//            });

//            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
//            {
//                Id = OWNER_ID,
//                Email = "owner@d.com",
//                NormalizedEmail = "OWNER@D.COM",
//                UserName = "Owner@d.com",
//                NormalizedUserName = "OWNER@D.COM",
//                EmailConfirmed = false,
//                PasswordHash = hasher.HashPassword(null, "Test123!"),
//                SecurityStamp = "d7ef2aca-70e6-4830-b73b-7d367223d224",
//                SalesGroupID = 1
//            });

//            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
//            {
//                Id = MANAGER_ID,
//                Email = "manager@d.com",
//                NormalizedEmail = "MANAGER@D.COM",
//                UserName = "Manager@d.com",
//                NormalizedUserName = "MANAGER@D.COM",
//                EmailConfirmed = false,
//                PasswordHash = hasher.HashPassword(null, "Test123!"),
//                SecurityStamp = "2db28a1c-1ca1-4b59-a44a-20cf18609493",
//                SalesGroupID = 1
//            });


//            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
//            {
//                Id = USER_ID,
//                Email = "user@d.com",
//                NormalizedEmail = "USER@D.COM",
//                UserName = "User@d.com",
//                NormalizedUserName = "USER@D.COM",
//                EmailConfirmed = false,
//                PasswordHash = hasher.HashPassword(null, "Test123!"),
//                SecurityStamp = "f285f38c-3c2d-469b-9a91-b40fd6c559a5",
//                SalesGroupID = 1
//            });
//#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

//            //Seeding the relation between our user and role to AspNetUserRoles table
//            builder.Entity<IdentityUserRole<string>>().HasData(
//                new IdentityUserRole<string>
//                {
//                    RoleId = Administrator_Role_Id,
//                    UserId = ADMIN_ID
//                });

//            builder.Entity<IdentityUserRole<string>>().HasData(
//                new IdentityUserRole<string>
//                {
//                    RoleId = Owner_Role_Id,
//                    UserId = OWNER_ID
//                });

//            builder.Entity<IdentityUserRole<string>>().HasData(
//                new IdentityUserRole<string>
//                {
//                    RoleId = Manager_Role_Id,
//                    UserId = MANAGER_ID
//                });

//            builder.Entity<IdentityUserRole<string>>().HasData(
//               new IdentityUserRole<string>
//               {
//                   RoleId = User_Role_Id,
//                   UserId = USER_ID
//               });

//        }
    }
}
