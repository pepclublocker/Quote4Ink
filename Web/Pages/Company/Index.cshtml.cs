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
    public class CompanyModel(UserManager<ApplicationUser> myUser, IRepository repository) : PageModel
    {
        // public readonly ILogger<ClientsModel>? _logger = logger;
        public IRepository? _repRepository { get; } = repository;

        public UserManager<ApplicationUser>? _appUser = myUser;

        public IEnumerable<Web.Data.Models.Company> pCompany = new List<Web.Data.Models.Company>();

        [BindProperty]
        public Client? Input { get; set; }

        public IActionResult OnGetLoadInital()
        {
            var myUser = _appUser?.GetUserAsync(User).Result;
            if (myUser != null)
            {
                //grab all clients in database for display
                var companies = _repRepository?.GetCompanys(myUser.SalesGroupID);
                pCompany = companies ?? Enumerable.Empty<Web.Data.Models.Company>();

                return Partial("_ClientList", pCompany);
            }
            else
            {
                return Partial("_ErrorDialog", new Exception("User not found"));
            }

        }

        public IActionResult OnGetDeleteClient(Guid myCompanyID)
        {
            var myUser = _appUser?.GetUserAsync(User).Result;
            if (myUser != null)
            {
                var returnValue = _repRepository?.DeleteClient(myCompanyID, myUser.SalesGroupID).ToString();

                if (returnValue == "True") //the delete did work
                {
                    var companies = _repRepository?.GetCompanys(myUser.SalesGroupID);
                    pCompany = companies ?? Enumerable.Empty<Web.Data.Models.Company>();
                    return Partial("_ClientList", pCompany);
                }
                else //the delete did not work 
                {
                    return Partial("_ErrorDialog", new Exception("Company not deleted"));
                }
            }
            else
            {
                return Partial("_ErrorDialog", new Exception("User not found"));
            }
        }

        public IActionResult OnPostDelete(string id)
        {
            string returnValue = "False";
            var myUser = _appUser?.GetUserAsync(User).Result;
            if (myUser != null)
            {
                List<Web.Data.Models.Company> companies = new List<Web.Data.Models.Company>();
                try //lets try all the data query first and if any of it fails we can just return the error modal 
                {
                    companies = (_repRepository?.GetCompanys(myUser.SalesGroupID) ?? Enumerable.Empty<Web.Data.Models.Company>()).ToList(); //list of clients


                    Web.Data.Models.Company? thisCompany = companies.FirstOrDefault(x => x.Id == Guid.Parse(id)); //the client we are deleting

                    var deleteResult = _repRepository?.DeleteCompany(Guid.Parse(id), myUser.SalesGroupID);
                    returnValue = deleteResult?.ToString() ?? "False"; //attempt to delete

                    if (returnValue == "True")
                    {
                        if (thisCompany != null)
                        {
                            _ = companies.Remove(thisCompany); //final list if we succeed in deleting the client
                        }
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
                    return Partial("_CompanyList", companies);
                }
                else  //error --send back full list and the error dialog to display the error message that item did not delete - maybe error message from repository
                {

                    return Partial("_NotDeleteDialog", companies);
                    //log issue here with data
                }
            }
            else
            {
                return Partial("_ErrorDialog", new Exception("User not found"));
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

        public IActionResult OnGetCompanyEdit(string id)
        {
            var myUser = _appUser?.GetUserAsync(User).Result;
            if (myUser != null)
            {
                //this gets the info about the client we want to delete so we can display it in the confirmation modal
                var thisCompany = _repRepository?.GetCompany(Guid.Parse(id), myUser.SalesGroupID);
                return Partial("_ClientManage", thisCompany);
            }
            else
            {
                return Partial("_ErrorPopup", new Exception("Issue with company or with edit"));
            }
        }

        public IActionResult OnPostUpdate(Web.Data.Models.Company Input)
        {
            //this updates the client based on the info sent to the form 

            var myUser = _appUser?.GetUserAsync(User).Result;
            if (myUser != null)
            {
                Web.Data.Models.Company oldCompany = _repRepository?.GetCompany(Input.Id, myUser.SalesGroupID) ?? new Web.Data.Models.Company();

                //if oldCompany is null - this maybe a new company - check on this 
                var comparer = new ObjectsComparer.Comparer<Web.Data.Models.Company>();
                IEnumerable<Difference> differences;
                comparer.IgnoreMember<DateTime>();

                if (!comparer.Compare(Input, oldCompany, out differences))
                {
                    _repRepository?.UpdateCompany(Input);
                }

                var companies = _repRepository?.GetCompanys(myUser.SalesGroupID);
                pCompany = companies ?? Enumerable.Empty<Web.Data.Models.Company>();

                return Partial("_ClientList", pCompany);
            }
            else
            {
                return Partial("_ErrorDialog", new Exception("Company List Not Found"));
            }
        }
    }
}