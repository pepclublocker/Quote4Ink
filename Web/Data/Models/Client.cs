using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models
{
   
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string? Email { get; set; }

        public string? Address { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        [Required]
        public bool Privledged { get; set; } = false;// indicated if can it be deleted?

        //add phone ????

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        [Column("fkCompany")]
        [ForeignKey("Company")]
        public Guid CompanyID { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Company Company { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        public bool Active { get; set; } = true;

       // public List<_DQuote> Quotes { get; set; } = new List<_DQuote>();

    }
}
