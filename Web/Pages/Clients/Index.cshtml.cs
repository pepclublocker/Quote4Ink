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
    public class ClientsModel(UserManager<ApplicationUser> myUser, ILogger<ClientsModel> logger, IRepository repository) : PageModel
    {

        public readonly ILogger<ClientsModel>? _logger;
        public IRepository? _repRepository { get; }

        public UserManager<ApplicationUser>? _appUser;

        public IEnumerable<Client> pClients { get; set; }

        public void OnGet()
        {


          //  pClients = _repRepository.GetClients(_appUser.GetUserAsync(User).Result.SalesGroupID);

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
    }
}



