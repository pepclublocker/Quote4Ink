namespace Web.Data.Repository
{
    public partial interface IRepository
    {
        public List<System.Collections.Generic.KeyValuePair<Web.Data.Models.ApplicationUser, System.Collections.Generic.List<Microsoft.AspNetCore.Identity.IdentityRole>>> GetMyUsers();

        // public ApplicationUserApiKey GetApiKey(IdentityUser user);

    }
}
