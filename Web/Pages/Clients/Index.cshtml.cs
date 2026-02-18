using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Formats.Asn1;
using Web.Data.Models;
using Web.Data.Repository;


namespace Web.Pages
{
    [Authorize]
    public class ClientsModel(UserManager<ApplicationUser> myUser, IRepository repository) : PageModel
    {
        // public readonly ILogger<ClientsModel>? _logger = logger;
        public IRepository? _repRepository { get; } = repository;

        public UserManager<ApplicationUser>? _appUser = myUser;

        public IEnumerable<Client> pClients { get; set; }


        public void OnGet()
        {
            //grab all clients in database for display
            pClients = _repRepository.GetClients(_appUser.GetUserAsync(User).Result.SalesGroupID);
        }

        public ActionResult OnGetDeleteClient(Guid myClientID)
        {
            var returnValue = _repRepository.DeleteClient(myClientID, _appUser.GetUserAsync(User).Result.SalesGroupID).ToString();

            if (returnValue == "True") //the delete did work
            {
                pClients = _repRepository.GetClients(_appUser.GetUserAsync(User).Result.SalesGroupID).ToList();
                return Partial("_ClientList", pClients);
            }
            else //the delete did not work 
            {
                return new JsonResult(returnValue);
            }
        }

        public IActionResult OnPostDelete(string id)
        {
            string returnValue = "False";
            Client thisClient = new Client();
            List<Client> clients = new List<Client>();
            try //lets try all the data query first and if any of it fails we can just return the error modal 
            {
                clients = _repRepository.GetClients(_appUser.GetUserAsync(User).Result.SalesGroupID).ToList(); //list of clients
                thisClient = (Client)clients.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault(); //the client we are deleting
                returnValue = _repRepository.DeleteClient(Guid.Parse(id), _appUser.GetUserAsync(User).Result.SalesGroupID).ToString(); //attempt to delete
                if (returnValue == "True")
                {
                    clients.Remove(thisClient); //final list if we succeed in deleting the client
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                // For example: _logger.LogError(ex, "Error deleting client with ID {ClientId}", id);
                //return new JsonResult(new { success = false, message = "An error occurred while deleting the client." });
                return Partial("_ErrorDialog", ex);

            }
           

            if (returnValue == "True") //the delete did work - return the partial with the updated client list to update the UI
            {
                return Partial("_ClientList", clients);
            }
            else  //error --send back full list and the error dialog to display the error message that item did not delete - maybe error message from repository
            {
               
                return  Partial("_NotDeleteDialog", clients);
                //log issue here with data
            }
        }

        public ActionResult OnGetConfirmDelete(string id)
        {
            //this gets the info about the client we want to delete so we can display it in the confirmation modal
            var thisClient = _repRepository.GetClient(Guid.Parse(id), _appUser.GetUserAsync(User).Result.SalesGroupID);
            return Partial("_ConfirmDelete", thisClient);
        }

        public IActionResult OnGetClientEdit(string id)
        {
            //this gets the info about the client we want to delete so we can display it in the confirmation modal
            var thisClient = _repRepository.GetClient(Guid.Parse(id), _appUser.GetUserAsync(User).Result.SalesGroupID);
            return Partial("_ConfirmDelete", thisClient);
        }
    }
}

















