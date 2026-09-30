using Microsoft.EntityFrameworkCore;
using Web.Data.Models;

namespace Web.Data.Repository
{
    public partial class SQLRepository : IRepository

    {

        public IQueryable<Client> GetClients(int __salesGroup, bool ActiveUsers = true)
        {
            // int[] CompList = _context.Companies.Where(y => y.SalesGroupID == __salesGroup).Select(u => u.Id).ToArray().con;

          //  return _context.Clients.Where(x => x.Active == ActiveUsers && _context.Companies.Where(y => y.SalesGroupID == __salesGroup);

            return _context.Clients.Where(x => x.Active == ActiveUsers && _context.Companies.Where(y => y.SalesGroupID == __salesGroup).Select(u => u.Id).ToArray().Contains(x.CompanyID)).Include(a => a.Company);
            // return _context.Clients.Where(x => x)

        }

        public Client GetClient(Guid ClientID, int __salesGroup)
        {
            //conditions
            //-must be in sale group of user
            //-must have id selected

            return _context.Clients
                .Where(x => x.Id == ClientID && _context.Companies.Any(company => company.Id == x.CompanyID && company.SalesGroupID == __salesGroup))
                .FirstOrDefault() ?? new Client();
        }

        public void SaveClient(Client client)
        {

            client.DateUpdated = DateTime.Now;
            _context.Clients.Add(client);
            _context.SaveChanges();
        }
        public bool UpdateClient(Client client, int __salesGroup)
        {
            var originalClient = _context.Clients
                .Where(x => x.Id == client.Id && _context.Companies.Any(company => company.Id == x.CompanyID && company.SalesGroupID == __salesGroup))
                .FirstOrDefault();

            if (originalClient == null)
            {
                return false;
            }

            originalClient.FirstName = client.FirstName;
            originalClient.LastName = client.LastName;
            originalClient.Email = client.Email;
            originalClient.Address = client.Address;
            originalClient.ZipCode = client.ZipCode;
            originalClient.City = client.City;
            originalClient.State = client.State;
            originalClient.DateUpdated = DateTime.Now;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteClient(Guid ClientID, int __salesGroup)
        { 
            var xRows = _context.Clients
                .Where(x => x.Id == ClientID && _context.Companies.Any(company => company.Id == x.CompanyID && company.SalesGroupID == __salesGroup))
                .FirstOrDefault();

            if (xRows != null)
            {
                xRows.Active = false;
                _context.ChangeTracker.Clear();
                _context.Clients.Update(xRows);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        IEnumerable<Client> IRepository.GetClients(int __salesGroup, bool ActiveUsers)
        {
            return GetClients(__salesGroup, ActiveUsers);
        }
    }
}
