using Microsoft.EntityFrameworkCore;
using Web.Data.Models;
//using k8s.KubeConfigModels;
//using YamlDotNet.Core;
//using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Web.Data.Repository
{
    public partial class SQLRepository : IRepository
    {

        public IEnumerable<Company> GetCompanys(int __salesGroup)
        {
            //conditions
            //-must be in sale group of user
            return _context.Companies.Where(x => x.SalesGroupID == __salesGroup);//.Include(i => i.SalesGroup).Include(y => y.Clients);
        }

        public void SaveCompany(Company company)
        {
            company.DateCreated = DateTime.Now;
            company.DateUpdated = DateTime.Now;

            _context.Companies.Add(company);
            _context.SaveChanges();
        }

        public void UpdateCompany(Company company)
        {

            Company OriginalCompany = _context.Companies.Where(x => x.SalesGroupID == company.SalesGroupID && x.Id == company.Id).FirstOrDefault() ?? new Company();

            company.DateCreated = OriginalCompany.DateCreated;
            company.Privledged = OriginalCompany.Privledged;

            OriginalCompany = company;
            OriginalCompany.DateUpdated = DateTime.Now;


            _context.ChangeTracker.Clear();
            _context.Companies.Update(OriginalCompany);
            _context.SaveChanges();
        }

        public Company GetCompany(Guid CompanyID, int __salesGroup)
        {
            //conditions
            //-must be in sale group of user
            //-must have id selected

            return _context.Companies.Where(x => x.SalesGroupID == __salesGroup && x.Id == CompanyID).AsNoTracking().FirstOrDefault() ?? new Company();
        }

        public bool DeleteCompany(Guid CompanyID, int __salesGroup)
        {

            //conditions for delete
            //-in the users sale group
            //-the id matches
            //-no clients attached

            var xString = false;
            var xRows = _context.Companies.Where(x => x.SalesGroupID == __salesGroup && x.Id == CompanyID).ExecuteDelete();
            //var xRows = _context.Companies.Where(x => x.SalesGroupID == __salesGroup && x.Id == CompanyID && x.Clients.Count() < 1).ExecuteDelete();

            if (xRows > 0)
                xString = true;

            return xString;
        }

    }
}