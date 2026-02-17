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
            
            
            var x = 1;
            pClients = _repRepository.GetClients(_appUser.GetUserAsync(User).Result.SalesGroupID);
            
            
            
            var y=1;
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
            var x = "this is good stuff";
            //return new JsonResult(x);
            _repRepository.DeleteClient(Guid.Parse(id), _appUser.GetUserAsync(User).Result.SalesGroupID);

            return Partial("_ClientList", _repRepository.GetClients(_appUser.GetUserAsync(User).Result.SalesGroupID).ToList());
        }


    }
}





