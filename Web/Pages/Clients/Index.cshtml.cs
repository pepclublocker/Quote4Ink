using Htmx.Net.Toast.Abstractions;
using Htmx.Net.Toast.Enums;
using Htmx.Net.Toast.Notyf.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ObjectsComparer;
using System.Formats.Asn1;
using Web.Data.Models;
using Web.Data.Repository;

namespace Web.Pages
{
    [Authorize]
    public class ClientsModel(UserManager<ApplicationUser> myUser, IRepository repository, INotyfService notifyS) : PageModel
    {
        // public readonly ILogger<ClientsModel>? _logger = logger;
        public IRepository? _repRepository { get; } = repository;

        public UserManager<ApplicationUser>? _appUser = myUser;

        public IEnumerable<Client> pClients = new List<Client>();

        public INotyfService _notyf { get; } = notifyS;

        [BindProperty]
        public Client? Input { get; set; }


        public IActionResult OnGetLoadInital()
        {

            var myUser = _appUser?.GetUserAsync(User).Result;

            if (myUser != null)
            {

                //grab all clients in database for display
                var clients = _repRepository?.GetClients(myUser.SalesGroupID);
                pClients = clients ?? Enumerable.Empty<Web.Data.Models.Client>();
                return Partial("_ClientList", pClients);
            }
            else
            {
                _notyf.Error("An <strong>ERROR</strong> has occured!. <br>You may need to check if you are logged in");
                return Partial("_ErrorDialog", new Exception("User not found"));
            }
        }


        public IActionResult OnPostDelete(string id)
        {
            string returnValue = "False";
            var myUser = _appUser?.GetUserAsync(User).Result;
            if (myUser != null)
            {
                List<Client> clients = new List<Client>();
                try //lets try all the data query first and if any of it fails we can just return the error modal 
                {
                    clients = (_repRepository?.GetClients(myUser.SalesGroupID) ?? Enumerable.Empty<Client>()).ToList(); //list of clients

                    Client? thisClient = clients.FirstOrDefault(x => x.Id == Guid.Parse(id)); //the client we are deleting

                    var deleteResult = _repRepository?.DeleteClient(Guid.Parse(id), myUser.SalesGroupID); //attempt to delete
                    returnValue = deleteResult?.ToString() ?? "False"; //attempt to delete

                    if (returnValue == "True")
                    {
                        if (thisClient != null)
                        {
                            _ = clients.Remove(thisClient); //final list if we succeed in deleting the client
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                    // For example: _logger.LogError(ex, "Error deleting client with ID {ClientId}", id);
                    //return new JsonResult(new { success = false, message = "An error occurred while deleting the client." });
                    _notyf.Error("An <strong>ERROR</strong> has occured!. <br>An issue happened with deleting this client");
                    return Partial("_ErrorDialog", ex);

                }


                if (returnValue == "True") //the delete did work - return the partial with the updated client list to update the UI
                {
                    _notyf.Success("Client was deleted and data integrity maintained.");
                    return Partial("_ClientList", clients);
                }
                else  //error --send back full list and the error dialog to display the error message that item did not delete - maybe error message from repository
                {
                    _notyf.Success("Client was <strong>NOT</strong> deleted.");
                    return Partial("_NotDeleteDialog", clients);
                    //log issue here with data
                }
            }
            else
            {
                _notyf.Error("An <strong>ERROR</strong> has occured!. <br>You may need to check if you are logged in");
                return Partial("_ErrorDialog", new Exception("User not found"));
            }
        }

        public ActionResult OnGetConfirmDelete(string id)
        {
            var myUser = _appUser?.GetUserAsync(User).Result;
            if (myUser != null)
            {
                //this gets the info about the client we want to delete so we can display it in the confirmation modal
                var thisClient = _repRepository?.GetClient(Guid.Parse(id), myUser.SalesGroupID);
                return Partial("_ConfirmDelete", thisClient);
            }
            else
            {
                _notyf.Error("An <strong>ERROR</strong> has occured!. <br>You may need to check if you are logged in");
                return Partial("_ErrorPopup", new Exception("Issue with client or with delete"));
            }
        }

        public IActionResult OnGetClientEdit(string id)
        {

            var myUser = _appUser?.GetUserAsync(User).Result;
            var thisClient = new Client();
            if (myUser != null)
            {
                if (id != String.Empty && id != null)
                {
                    thisClient = _repRepository?.GetClient(Guid.Parse(id), myUser.SalesGroupID);
                }
                return Partial("_ClientManage", thisClient);
            }
            else
            {
                _notyf.Error("An <strong>ERROR</strong> has occured!. <br>You may need to check if you are logged in");
                return Partial("_ErrorPopup", new Exception("Issue with client or with edit"));
            }
        }

        public IActionResult OnPostUpdate(Client Input)
        {
            //this updates the client based on the info sent to the form 
            var myUser = _appUser?.GetUserAsync(User).Result;
            if (myUser != null)
            {

                var oldClient = _repRepository?.GetClient(Input.Id, myUser.SalesGroupID) ?? new Client();

                if (oldClient.Id != Input.Id)
                {
                    _notyf.Error("Client could not be updated.");
                }
                else
                {

                    var comparer = new ObjectsComparer.Comparer<Client>();
                    IEnumerable<Difference> differences;
                    comparer.IgnoreMember<DateTime>();

                    if (!comparer.Compare(Input, oldClient, out differences))
                    {
                        if (_repRepository?.UpdateClient(Input, myUser.SalesGroupID) == true)
                        {
                            _notyf.Success("Client edited and saved!");
                        }
                        else
                        {
                            _notyf.Error("Client could not be updated.");
                        }
                    }
                    else
                    {
                        _notyf.Information("No changes to client detected.<br> No update made.");
                    }
                }

                var clients = _repRepository?.GetClients(myUser.SalesGroupID);
                pClients = clients ?? Enumerable.Empty<Web.Data.Models.Client>();

                return Partial("_ClientList", pClients);
            }
            else
            {
                _notyf.Error("An <strong>ERROR</strong> has occured!. <br>You may need to check if you are logged in");
                return Partial("_ErrorDialog", new Exception("Client List Not Found"));
            }
        }
    }
}