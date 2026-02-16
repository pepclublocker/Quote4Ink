using Web.Data.Models;
//This is all repository stuff to do with clients - 



namespace Web.Data.Repository
{
    public partial interface IRepository
    {
        public IEnumerable<Client> GetClients(int __salesGroup, bool ActiveUsers = true);
        public Client GetClient(Guid ClientID, int __salesGroup);
        public void UpdateClient(Client client);
        public void SaveClient(Client client);

        public bool DeleteClient(Guid ClientID, int __salesGroup);
    }
}
