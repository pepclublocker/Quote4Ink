using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; } = "First Name";
        [PersonalData]
        public string LastName { get; set; } = "Last Name";
        public int UsernameChangeLimit { get; set; } = 10;
        [PersonalData]
        public byte[]? ProfilePicture { get; set; }
        [PersonalData]
        public string? Title { get; set; }

        [ForeignKey("SalesGroup")]
        public int SalesGroupID { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        //public _DSalesGroup SalesGroup { get; set; } //must have a sales group 
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        public int TwoFAType { get; set; }
        [PersonalData]

        public bool IsDeleted { get; set; } = false;

        [NotMapped]
        public bool LockedOut
        {
            get
            {

                if (LockoutEnd >= DateTimeOffset.Now)
                    return true;
                else
                    return false;
            }
            set
            { }

        }


    }
}
