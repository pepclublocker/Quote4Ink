using Htmx.Net.Toast.Abstractions;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Data.Models;
using Web.Data.Repository;


namespace Web.Pages
{
    [Authorize(Roles = "Administrator, Manager")]
    public class SalesGroupModel(UserManager<ApplicationUser> myUser, IRepository repository, INotyfService notifyS) : PageModel
    {
        public IRepository? _repRepository { get; } = repository;

        public UserManager<ApplicationUser>? _appUser = myUser;
        public Web.Data.Models.SalesGroup salesGroup { get; set; } = new Web.Data.Models.SalesGroup();

        public INotyfService _notyf { get; } = notifyS;

        [BindProperty]
        public Web.Data.Models.SalesGroup? Input { get; set; }


        public IActionResult OnGetLoadInital()
        {
            var myUser = _appUser?.GetUserAsync(User).Result;

            if (myUser != null)
            {
                //grab all clients in database for display
                var group = _repRepository?.GetMySalesGroup(myUser.SalesGroupID);
                if (group != null)
                {
                    salesGroup = group;
                    return Partial("_ShopInfo", salesGroup);
                }
                else
                {
                    _notyf.Error("An <strong>ERROR</strong> has occured!. <br>Sales group not found.");
                    return Partial("_ErrorDialog", new Exception("Sales group not found"));
                }
            }
            else
            {
                _notyf.Error("An <strong>ERROR</strong> has occured!. <br>You may need to check if you are logged in");
                return Partial("_ErrorDialog", new Exception("User not found"));
            }
        }

        public IActionResult OnGetShopManage()
        {
            var myUser = _appUser?.GetUserAsync(User).Result;
            if (myUser != null)
            {
                //grab all clients in database for display
                var group = _repRepository?.GetMySalesGroup(myUser.SalesGroupID);
                if (group != null)
                {
                    salesGroup = group;
                    return Partial("_ShopManage", salesGroup);
                }
                else
                {
                    _notyf.Error("An <strong>ERROR</strong> has occured!. <br>Sales group not found.");
                    return Partial("_ErrorDialog", new Exception("Sales group not found"));
                }
            }
            else
            {
                _notyf.Error("An <strong>ERROR</strong> has occured!. <br>You may need to check if you are logged in");
                return Partial("_ErrorDialog", new Exception("User not found"));
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var y in ModelState.Values)
                {

                }

                return Page();
            }

            Input.Id = (await _appUser.GetUserAsync(User)).SalesGroupID;
            _repRepository.UpdateSalesGroup(Input);



            return RedirectToPage();
            //return LocalRedirect("/SalesGroup/Index");
        }
    }
}