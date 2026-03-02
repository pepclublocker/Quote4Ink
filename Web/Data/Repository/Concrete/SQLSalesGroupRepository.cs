using Web.Data.Models;
//using k8s.KubeConfigModels;

namespace Web.Data.Repository
{
    public partial class SQLRepository : IRepository
    {
        public int CreateSalesGroup(string SalesGroupName, string SalesGroupEmail)
        {
            var salesGroup = new SalesGroup();
            salesGroup.SalesGroupName = SalesGroupName;
            salesGroup.SalesGroupEmail = SalesGroupEmail;

            _context.SalesGroups.Add(salesGroup);
            _context.SaveChanges();

            //need to create the default company that will house the clients with no company 
            var tempCompany = new Company();
            tempCompany.SalesGroupID = salesGroup.Id;
            tempCompany.Name = "No associated company (default)";
            tempCompany.Privledged = true;
            tempCompany.DateCreated = DateTime.Now;
            tempCompany.DateUpdated = DateTime.Now;

            _context.Companies.Add(tempCompany);
            _context.SaveChanges();

            //need to create the default user that will house the clients with no company 
            var tempClient = new Client();
            tempClient.FirstName = "Walk-ins";
            tempClient.LastName = "Store";
            tempClient.CompanyID = tempCompany.Id;
            tempClient.Privledged = true;
            tempClient.DateCreated = DateTime.Now;
            tempClient.DateUpdated = DateTime.Now;
            tempClient.Active = true;

            _context.Clients.Add(tempClient);
            _context.SaveChanges();


            return salesGroup.Id;
        }

        public void UpdateSalesGroup(SalesGroup __salesGroup)
        {
            _context.Update(__salesGroup);
            _context.SaveChanges();
        }

        public SalesGroup GetMySalesGroup(int ii__salesGroup)
        {
            return _context.SalesGroups.FirstOrDefault(x => x.Id == ii__salesGroup);
        }

        public SalesGroup GetStripeSalesGroup(string StripCustomerID)
        {
            return _context.SalesGroups.FirstOrDefault(x => x.StripeAccountNumber == StripCustomerID);
        }


    }
}
