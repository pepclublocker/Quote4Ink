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


        public async Task<IActionResult> OnGetLoadInital()
        {           
            var currentUser = await _appUser.GetUserAsync(User);
            if (currentUser == null)
            {
                return Forbid();
            }

            if (_repRepository != null)
            {
                Users = _repRepository.GetMyUsers(currentUser.SalesGroupID);
            }

            return Partial("_UserList", Users);
        }

        public async Task<IActionResult> OnPostDeleteUser(string UserID)
        {
            var currentUser = await _appUser.GetUserAsync(User);
            if (currentUser == null)
            {
                return Forbid();
            }

            var delUser = await _appUser.FindByIdAsync(UserID);
            if (delUser == null)
            {
                return new JsonResult(new { error = "User not found." });
            }

            if (delUser.SalesGroupID != currentUser.SalesGroupID)
            {
                return Forbid();
            }

            var result = await _appUser.DeleteAsync(delUser); //make this a soft delete?

            _appUser.UpdateSecurityStampAsync(delUser);

            if (result.Succeeded)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                Users = _repRepository.GetMyUsers(currentUser.SalesGroupID);
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
