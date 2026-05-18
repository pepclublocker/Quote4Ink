using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Data.Models;
using Web.Data.Repository;

namespace Web.Pages
{
    [Authorize(Roles = "Administrator, Manager")]
    public class UserIndexModel(UserManager<ApplicationUser> myUser, IRepository repository) : PageModel
    {

        public IRepository? _repRepository { get; } = repository;

        public UserManager<ApplicationUser>? _appUser = myUser;

        public List<System.Collections.Generic.KeyValuePair<Web.Data.Models.ApplicationUser, System.Collections.Generic.List<Microsoft.AspNetCore.Identity.IdentityRole>>> Users { get; private set; } = new List<System.Collections.Generic.KeyValuePair<Web.Data.Models.ApplicationUser, System.Collections.Generic.List<Microsoft.AspNetCore.Identity.IdentityRole>>>();


        public IActionResult OnGetLoadInital()
        {           
            // Ensure _repRepository is not null before dereferencing
            if (_repRepository != null)
            {
                Users = _repRepository.GetMyUsers();
            }
            else
            {
                Users = new List<System.Collections.Generic.KeyValuePair<Web.Data.Models.ApplicationUser, System.Collections.Generic.List<Microsoft.AspNetCore.Identity.IdentityRole>>>();
            }

            return Partial("_UserList", Users);
        }

        public IActionResult OnPostDeleteUser(string UserID)
        {
            if (_appUser == null)
            {
                return new JsonResult(new { error = "UserManager is not available." });
            }

            var delUser = _appUser.FindByIdAsync(UserID).Result;
            if (delUser == null)
            {
                return new JsonResult(new { error = "User not found." });
            }

            var result = _appUser.DeleteAsync(delUser).Result; //make this a soft delete?

            _appUser.UpdateSecurityStampAsync(delUser);

            if (result.Succeeded)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                Users = _repRepository.GetMyUsers();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                return Partial("_UserList", Users);
            }
            else
            {
                return new JsonResult(result);
            }
        }

        public IActionResult OnGetConfirmDelete(string id)
        {
            var myUser = _appUser?.GetUserAsync(User).Result;
            if (myUser != null)
            {
                //this gets the info about the client we want to delete so we can display it in the confirmation modal
                var thisCompany = _repRepository?.GetCompany(Guid.Parse(id), myUser.SalesGroupID);
                return Partial("_ConfirmDelete", thisCompany);
            }
            else
            {
                return Partial("_ErrorPopup", new Exception("Issue with company or with delete"));
            }
        }
    }
}
