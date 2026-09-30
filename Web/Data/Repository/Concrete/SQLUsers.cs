using Microsoft.AspNetCore.Identity;
using Web.Data.Models;

namespace Web.Data.Repository
{
    public partial class SQLRepository : IRepository
    {

        public List<System.Collections.Generic.KeyValuePair<Web.Data.Models.ApplicationUser, System.Collections.Generic.List<Microsoft.AspNetCore.Identity.IdentityRole>>> GetMyUsers(int __salesGroup)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            List<System.Collections.Generic.KeyValuePair<Web.Data.Models.ApplicationUser, System.Collections.Generic.List<Microsoft.AspNetCore.Identity.IdentityRole>>> myUsers = _context.Users
     .Where(user => user.SalesGroupID == __salesGroup)
     .SelectMany(
         // -- below emulates a left outer join, as it returns DefaultIfEmpty in the collectionSelector
         user => _context.UserRoles.Where(userRoleMapEntry => user.Id == userRoleMapEntry.UserId).DefaultIfEmpty(),
         (user, roleMapEntry) => new { User = user, RoleMapEntry = roleMapEntry })
     .SelectMany(
         // perform the same operation to convert role IDs from the role map entry to roles
         x => _context.Roles.Where(role => role.Id == x.RoleMapEntry.RoleId).DefaultIfEmpty(),
         (x, role) => new { User = x.User, Role = role })
     .ToList() // runs the queries and sends us back into EF Core LINQ world
     .Aggregate(
         new Dictionary<ApplicationUser, List<IdentityRole>>(), // seed
         (dict, data) =>
         {
             // safely ensure the user entry is configured
             dict.TryAdd(data.User, new List<IdentityRole>());
             if (null != data.Role)
             {
                 dict[data.User].Add(data.Role);
             }
             return dict;
         },
         x => x).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return myUsers;
        }


        //public ApplicationUserApiKey GetApiKey(IdentityUser user)
        //{
        //    var newApiKey = new ApplicationUserApiKey
        //    {
        //        User = user,
        //        Value = GenerateApiKeyValue()
        //    };

        //    _context.ApplicationUserApiKeys.Add(newApiKey);

        //    _context.SaveChanges();

        //    return newApiKey;
        //}

        //private string GenerateApiKeyValue() =>
        //  $"{Guid.NewGuid().ToString()}-{Guid.NewGuid().ToString()}";

        // GetMyUsers
    }
}
