using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class Migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlankCategories",
                columns: table => new
                {
                    categoryID = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlankCategories", x => x.categoryID);
                });

            migrationBuilder.CreateTable(
                name: "BlankProducts",
                columns: table => new
                {
                    skuID_Master = table.Column<int>(type: "int", nullable: false),
                    sku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yourSku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    styleID = table.Column<int>(type: "int", nullable: true),
                    brandName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    styleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    colorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorPriceCodeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorFamilyID = table.Column<int>(type: "int", nullable: true),
                    colorSwatchImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorSwatchTextColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorFrontImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorSideImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorBackImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorDirectSideImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorOnModelFrontImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorOnModelSideImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorOnModelBackImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    color1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    color2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sizeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sizeCode = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    sizeOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sizePriceCodeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    caseQty = table.Column<int>(type: "int", nullable: false),
                    unitWeight = table.Column<double>(type: "float", nullable: false),
                    mapPrice = table.Column<double>(type: "float", nullable: false),
                    piecePrice = table.Column<double>(type: "float", nullable: false),
                    dozenPrice = table.Column<double>(type: "float", nullable: false),
                    casePrice = table.Column<double>(type: "float", nullable: false),
                    salePrice = table.Column<double>(type: "float", nullable: false),
                    customerPrice = table.Column<double>(type: "float", nullable: false),
                    saleExpiration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    noeRetailing = table.Column<bool>(type: "bit", nullable: false),
                    caseWeight = table.Column<double>(type: "float", nullable: false),
                    caseWidth = table.Column<double>(type: "float", nullable: false),
                    caseLength = table.Column<double>(type: "float", nullable: false),
                    caseHeight = table.Column<double>(type: "float", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    countryOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateLastChanged = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlankProducts", x => x.skuID_Master);
                });

            migrationBuilder.CreateTable(
                name: "BlankSanMar",
                columns: table => new
                {
                    UNIQUE_KEY = table.Column<int>(type: "int", nullable: false),
                    PRODUCT_TITLE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUCT_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STYLE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AVAILABLE_SIZES = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BRAND_LOGO_IMAGE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    THUMBNAIL_IMAGE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COLOR_SWATCH_IMAGE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUCT_IMAGE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SPEC_SHEET = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRICE_TEXT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SUGGESTED_PRICE = table.Column<double>(type: "float", nullable: false),
                    CATEGORY_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SUBCATEGORY_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COLOR_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COLOR_SQUARE_IMAGE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COLOR_PRODUCT_IMAGE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COLOR_PRODUCT_IMAGE_THUMBNAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SIZE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SIZE_SORT = table.Column<int>(type: "int", nullable: false),
                    PIECE_WEIGHT = table.Column<double>(type: "float", nullable: false),
                    PIECE_PRICE = table.Column<double>(type: "float", nullable: false),
                    DOZENS_PRICE = table.Column<double>(type: "float", nullable: false),
                    CASE_PRICE = table.Column<double>(type: "float", nullable: false),
                    PRICE_GROUP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CASE_SIZE = table.Column<int>(type: "int", nullable: false),
                    INVENTORY_KEY = table.Column<int>(type: "int", nullable: false),
                    SIZE_INDEX = table.Column<int>(type: "int", nullable: false),
                    SANMAR_MAINFRAME_COLOR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MILL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUCT_STATUS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COMPANION_STYLE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MSRP = table.Column<double>(type: "float", nullable: false),
                    MAP_PRICING = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FRONT_MODEL_IMAGE_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BACK_MODEL_IMAGE_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FRONT_FLAT_IMAGE_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BACK_FLAT_IMAGE_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUCT_MEASUREMENTS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PMS_COLOR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GTIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DECORATION_SPEC_SHEET = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateLastChanged = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlankSanMar", x => x.UNIQUE_KEY);
                });

            migrationBuilder.CreateTable(
                name: "BlankStyles",
                columns: table => new
                {
                    styleID = table.Column<int>(type: "int", nullable: false),
                    partNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    styleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uniqueStyleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    baseCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catalogPageNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newStyle = table.Column<bool>(type: "bit", nullable: false),
                    comparableGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    companionGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brandImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    styleImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    noeRetailing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    boxRequired = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateLastChanged = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlankStyles", x => x.styleID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlankCategories");

            migrationBuilder.DropTable(
                name: "BlankProducts");

            migrationBuilder.DropTable(
                name: "BlankSanMar");

            migrationBuilder.DropTable(
                name: "BlankStyles");
        }
    }
}
