using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Web.Data.Models;

namespace Web.Data
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
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
    }
}
