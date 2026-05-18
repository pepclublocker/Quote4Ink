using System.ComponentModel.DataAnnotations;
//using Microsoft.OpenApi.Attributes;

namespace Web.Data.Models.Enums
{
    public enum PropertyTypes
    {
        [Display(Name = "Per Screen")]
        PerScreen,
        [Display(Name = "Per Shirt")]
        PerShirt,
        [Display(Name = "One Time")]
        OneTime,
        [Display(Name = "Hourly")]
        Hourly
    }

}
