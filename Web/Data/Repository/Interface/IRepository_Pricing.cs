using Web.Data.Models;

namespace Web.Data.Repository
{
    public partial interface IRepository
    {
        public IEnumerable<PriceMatrix> GetMatrixies(int __salesGroup, bool __includeInactive = true);


    }
}
