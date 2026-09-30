using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Data.Models;

namespace Web.Data.Repository
{
    public partial class SQLRepository : IRepository
    {
        public IQueryable<PriceMatrix> GetMatrixies(int __salesGroup, bool __includeInactive = true)
        {
            // int[] CompList = _context.Companies.Where(y => y.SalesGroupID == __salesGroup).Select(u => u.Id).ToArray().con;

            //  return _context.Clients.Where(x => x.Active == ActiveUsers && _context.Companies.Where(y => y.SalesGroupID == __salesGroup);

            var myX = _context.PriceMatrices.Where(x => x.IsActive == __includeInactive && x.SalesGroupID == __salesGroup).ToList();
            return myX.AsQueryable();
            // return _context.Clients.Where(x => x)

        }

        IEnumerable<PriceMatrix> IRepository.GetMatrixies(int __salesGroup, bool __includeInactive = true)
       
        {
            return GetMatrixies(__salesGroup, __includeInactive);
        }

    }
}
