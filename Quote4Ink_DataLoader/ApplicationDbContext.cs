
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.ExceptionServices;

namespace Quote4Ink.Data
{
    internal class ApplicationDbContext : DbContext
    {
        

        public DbSet<Web.Data.Models.BlankStyle> Styles { get; set; }

        public DbSet<Web.Data.Models.BlankCategory> Categories { get; set; }

        public DbSet<Web.Data.Models.BlankProduct> Products { get; set; }

        public DbSet<Web.Data.Models.BlankSanMar> SanMars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-Web-31e576fb-e364-4599-9a47-84dcb78c55e4;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
    }
}
