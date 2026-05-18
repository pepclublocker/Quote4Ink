
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Data.Models.Enums;

namespace Web.Data.Models
{
  
    public class PriceMatrixProperties
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("SalesGroup")]
        public int SalesGroupID { get; set; } = 0;

        [Required]
        public string PropertyName { get; set; } = string.Empty;

        public PropertyTypes PropertyType { get; set; } //should we use an enum here? (per screen/color, per shirt, one time/per project)

        [Column(TypeName = "decimal(18,2)")]
        public decimal PropertyAmount { get; set; } = 0.00M;

        public int LowerThreshold { get; set; } = 1; //hoiw many it takes to activate the property - default 1
        public int? UpperThreshold { get; set; } //how many untill we stop the property - upper limit is 0 which is unlimited
        public bool IsActive { get; set; } = true;
        public string Descript { get; set; } = string.Empty;

    }
}
