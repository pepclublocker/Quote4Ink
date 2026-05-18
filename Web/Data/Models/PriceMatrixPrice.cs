using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models
{ 

    public class PriceMatrixPrice
    {
        private decimal otherPrice;

        public int Id { get; set; }

        public int Color { get; set; } // must have price matrix color


        public int LevelMaxCount { get; set; } // must have price matrix level


        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set => field = value.SetScale(2); }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OtherPrice { get => otherPrice; set => otherPrice = value.SetScale(2); }
        [ForeignKey("PriceMatrix")]
        public Guid MatrixId { get; set; }
        public PriceMatrix PriceMatrix { get; set; } = new PriceMatrix();

    }
}
