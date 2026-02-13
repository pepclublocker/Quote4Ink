
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Web.Data.Models
{
   
    public class _Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Column("fkSalesGroup")]
       // [ForeignKey("SalesGroup")]
        public int SalesGroupID { get; set; } = 0;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
       // public _DSalesGroup SalesGroup { get; set; } //must have a sales group 
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

      // public List<_DClient> Clients { get; set; } = new List<_DClient>();

        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }

        //add phone ???

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [Required]
        public bool Privledged { get; set; } = false;// indicated if can it be deleted?

        [IgnoreDataMember]
        public int Sort
        {
            get
            {
                if (Privledged == true)
                    return 1;
                else
                    return 2;
            }

        }
    }
}
