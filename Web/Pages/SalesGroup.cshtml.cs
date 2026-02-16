using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    [Authorize(Roles = "Administrator, Manager")]
    public class SalesGroup : PageModel
    {
        public void OnGet()
        {

        }
    }
}
