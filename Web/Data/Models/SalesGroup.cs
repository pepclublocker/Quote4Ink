using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models
{
  
    public class _SalesGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SalesGroupName { get; set; } = string.Empty;

        public string? SandSAPIKey { get; set; }
        public string? SandSAccountNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public string SalesGroupEmail { get; set; } = string.Empty;

        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        //public ICollection<Store> Stores; - this is for later when we add stores for 'popup' type sales

        public string? StripePublicKey { get; set; } = string.Empty;
        public string? StripePrivateKey { get; set; } = string.Empty;
        /// <summary>
        ///Customer number from Stripe after setting up subscription
        /// </summary>
        public string? StripeAccountNumber { get; set; } = string.Empty;
        public string? StripeKey { get; set; } = string.Empty;
        /// <summary>
        /// UserID in this system of user who purchased
        /// </summary>
        public string? BuyingUser { get; set; } = string.Empty;

        public DateTime? DateSubscribed { get; set; }

    }
}
