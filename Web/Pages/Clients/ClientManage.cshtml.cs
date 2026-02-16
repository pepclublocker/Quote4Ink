using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Data.Models;
using Web.Data.Repository;

namespace Web.Pages
{
    [Authorize]
    public class ClientManageModel(UserManager<ApplicationUser> myUser, ILogger<ClientManageModel> logger, IRepository repository) : PageModel
    {
        public readonly ILogger<ClientManageModel>? _logger = logger;
        public IRepository? _repRepository { get; } = repository;

        public UserManager<ApplicationUser>? _appUser = myUser;

        [BindProperty]
        public Client Input { get; set; }

        public SelectList TagOptions { get; set; }

        //[FromQuery(Name = "ClientID")]
        [BindProperty(SupportsGet = true)]
        public Guid ClientID { get; set; } = Guid.Empty;


        public void OnGet()
        {

            if (ClientID == Guid.Empty)
            {
                Input = new Web.Data.Models.Client();
            }
            else
            {
                Input = GetClientInfo(ClientID, _appUser.GetUserAsync(User).Result.SalesGroupID);
            }

          //  TagOptions = new SelectList(_repRepository.GetCompanys(_appUser.GetUserAsync(User).Result.SalesGroupID), nameof(Company.Id), nameof(Company.Name));



        }

        public void OnPostUpdateClient()
        {
            if (ClientID != Guid.Empty)
            {
                var OriginalClient = GetClientInfo(ClientID, _appUser.GetUserAsync(User).Result.SalesGroupID);

                Input.Id = OriginalClient.Id;
                // Input.SalesGroupID = OriginalClient.SalesGroupID;

                //now update
                _repRepository.UpdateClient(Input);
            }
            else
            {
                //create new

                //   Input.SalesGroupID = _appUser.GetUserAsync(User).Result.SalesGroup;

                _repRepository.SaveClient(Input);
            }
            //redirect
            Response.Redirect("/Clients");
        }
        public Client GetClientInfo(Guid ClientID, int __SalesGroupID)
        {
            return _repRepository.GetClient(ClientID, __SalesGroupID);
        }
    }
}
