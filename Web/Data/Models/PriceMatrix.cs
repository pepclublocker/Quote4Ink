using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Web.Data.Models
{
    
    public class PriceMatrix
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string MatrixName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDefault { get; set; } = false;
        public int MinUnits { get; set; } = 12;
        public decimal Markup { get; set; } = 30; //this is percentage
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public int MaxColors { get; set; } = 1;
        [ForeignKey("SalesGroup")]
        public int SalesGroupID { get; set; } = 0;
        public SalesGroup SalesGroup { get; set; } = new SalesGroup();   //must have a sales group 
    }
}
