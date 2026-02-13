using Microsoft.AspNetCore.Identity;
using Web.Data.Models;

namespace Web.Data.Repository
{
    public partial class SQLRepository : IRepository
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private ILogger<SQLRepository> _logger;

        public SQLRepository(ApplicationDbContext context, UserManager<ApplicationUser> userMan, ILogger<SQLRepository> logger)
        {
            this._context = context;
            this._userManager = userMan;
            this._logger = logger;
        }

    }
}
