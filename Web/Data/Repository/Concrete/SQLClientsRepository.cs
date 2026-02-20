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

            return _context.Clients.Where(x => x.Id == ClientID).FirstOrDefault() ?? new Client();
        }

        public void SaveClient(Client client)
        {

            client.DateUpdated = DateTime.Now;
            _context.Clients.Add(client);
            _context.SaveChanges();
        }
        public void UpdateClient(Client client)
        {
            var OriginalClient = _context.Clients.Where(x => x.Id == client.Id).FirstOrDefault() ?? new Client();

            client.DateCreated = OriginalClient.DateCreated;
            client.Privledged = OriginalClient.Privledged;

            OriginalClient = client;

            OriginalClient.DateUpdated = DateTime.Now;


            _context.ChangeTracker.Clear();
            _context.Clients.Update(OriginalClient);
            _context.SaveChanges();

        }

        public bool DeleteClient(Guid ClientID, int __salesGroup)
        { 
            var xRows = _context.Clients.Where(x => x.Id == ClientID).FirstOrDefault();

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
