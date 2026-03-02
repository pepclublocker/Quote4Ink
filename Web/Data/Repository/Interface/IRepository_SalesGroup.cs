using Web.Data.Models;
//This is all repository stuff to do with sales group - 



namespace Web.Data.Repository
{
    public partial interface IRepository
    {
        public SalesGroup GetMySalesGroup(int __salesGroup);

        public SalesGroup GetStripeSalesGroup(string StripCustomerID);

        public void UpdateSalesGroup(SalesGroup salesGroup);
        public int CreateSalesGroup(string SalesGroupName, string SalesGroupEmail);
    }
}
