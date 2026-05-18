using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models
{
    [Table("BlankProducts")]
    public class BlankProduct
    {

        public string? sku { get; set; }
        public string? gtin { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int skuID_Master { get; set; }
        public string? yourSku { get; set; }
        public int? styleID { get; set; }
        [StringLength(50)]
        public string? brandName { get; set; }
        public string? styleName { get; set; }
        [StringLength(50)]
        public string? colorName { get; set; }
        public string? colorCode { get; set; }
        public string? colorPriceCodeName { get; set; }
        public string? colorGroup { get; set; }
        public string? colorGroupName { get; set; }
        public int? colorFamilyID { get; set; }
        public string? colorSwatchImage { get; set; }
        public string? colorSwatchTextColor { get; set; }
        public string? colorFrontImage { get; set; }
        public string? colorSideImage { get; set; }
        public string? colorBackImage { get; set; }
        public string? colorDirectSideImage { get; set; }
        public string? colorOnModelFrontImage { get; set; }
        public string? colorOnModelSideImage { get; set; }
        public string? colorOnModelBackImage { get; set; }
        public string? color1 { get; set; }
        public string? color2 { get; set; }
        public string? sizeName { get; set; }
        [StringLength(1)]
        public string sizeCode { get; set; }
        public string? sizeOrder { get; set; }
        public string? sizePriceCodeName { get; set; }
        public int caseQty { get; set; }
        public double unitWeight { get; set; }
        public double mapPrice { get; set; }
        public double piecePrice { get; set; }
        public double dozenPrice { get; set; }
        public double casePrice { get; set; }
        public double salePrice { get; set; }
        public double customerPrice { get; set; }
        public string? saleExpiration { get; set; }
        public bool noeRetailing { get; set; }
        public double caseWeight { get; set; }
        public double caseWidth { get; set; }
        public double caseLength { get; set; }
        public double caseHeight { get; set; }
        public int qty { get; set; }
        public string? countryOfOrigin { get; set; }

        public DateTime dateLastChanged { get; set; } = DateTime.Now;

        public string? SkuName => brandName  + " " + styleName + " " + colorName   ;
    }
}
