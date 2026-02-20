using Web.Data.Models;
//This is all repository stuff to do with company - 



namespace Web.Data.Repository
{
    public partial interface IRepository
    {
        public IEnumerable<Company> GetCompanys(int __salesGroup);
        public Company GetCompany(Guid CompanyID, int __salesGroup);
        public void SaveCompany(Company company);
        public void UpdateCompany(Company company);
        public bool DeleteCompany(Guid CompanyID, int __salesGroup);
    }
}
