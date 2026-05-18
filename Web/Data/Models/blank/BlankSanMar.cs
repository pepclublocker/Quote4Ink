using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Models
{
    [Table("BlankSanMar")]
    public class BlankSanMar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UNIQUE_KEY { get; set; }
        public string PRODUCT_TITLE { get; set; }
        public string PRODUCT_DESCRIPTION { get; set; }
        public string STYLE { get; set; }
        public string AVAILABLE_SIZES { get; set; }
        public string BRAND_LOGO_IMAGE { get; set; }
        public string THUMBNAIL_IMAGE { get; set; }
        public string COLOR_SWATCH_IMAGE { get; set; }
        public string PRODUCT_IMAGE { get; set; }
        public string SPEC_SHEET { get; set; }
        public string PRICE_TEXT { get; set; }
        public double SUGGESTED_PRICE { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string SUBCATEGORY_NAME { get; set; }
        public string COLOR_NAME { get; set; }
        public string COLOR_SQUARE_IMAGE { get; set; }
        public string COLOR_PRODUCT_IMAGE { get; set; }
        public string COLOR_PRODUCT_IMAGE_THUMBNAIL { get; set; }
        public string SIZE { get; set; }
        public int SIZE_SORT
        {
            get
            {
              var  iSIZE_SORT = 0;
                switch (SIZE)
                {
                    case "S":
                        iSIZE_SORT = 1;
                        break;
                    case "M":
                        iSIZE_SORT = 2;
                        break;
                    case "L":
                        iSIZE_SORT = 3;
                        break;
                    case "XL":
                        iSIZE_SORT = 4;
                        break;
                    case "2XL":
                        iSIZE_SORT = 5;
                        break;
                    case "3XL":
                        iSIZE_SORT = 6;
                        break;
                    case "4XL":
                        iSIZE_SORT = 7;
                        break;
                    case "5XL":
                        iSIZE_SORT = 8;
                        break;
                    default:
                        iSIZE_SORT = 0;
                        break;
                }
                return iSIZE_SORT;

            }
            set { }
        }
        public double PIECE_WEIGHT { get; set; }
        public double PIECE_PRICE { get; set; }
        public double DOZENS_PRICE { get; set; }
        public double CASE_PRICE { get; set; }
        public string PRICE_GROUP { get; set; }
        public int CASE_SIZE { get; set; }
        public int INVENTORY_KEY { get; set; }
        public int SIZE_INDEX { get; set; }
        public string SANMAR_MAINFRAME_COLOR { get; set; }
        public string MILL { get; set; }
        public string PRODUCT_STATUS { get; set; }
        public string COMPANION_STYLE { get; set; }
        public double MSRP { get; set; }
        public string MAP_PRICING { get; set; }
        public string FRONT_MODEL_IMAGE_URL { get; set; }
        public string BACK_MODEL_IMAGE_URL { get; set; }
        public string FRONT_FLAT_IMAGE_URL { get; set; }
        public string BACK_FLAT_IMAGE_URL { get; set; }
        public string PRODUCT_MEASUREMENTS { get; set; }
        public string PMS_COLOR { get; set; }
        public string GTIN { get; set; }
        public string DECORATION_SPEC_SHEET { get; set; }

        public DateTime? dateLastChanged { get; set; } = DateTime.Now;

    }
}
