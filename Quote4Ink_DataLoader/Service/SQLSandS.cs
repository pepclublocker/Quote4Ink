using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Web.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Quote4Ink.Data
{
    internal partial class SQLRepository
    {

        /// <summary>
        /// Saves the specified style to the database, either by updating an existing record or adding a new one.
        /// </summary>
        /// <remarks>If a style with the same <c>styleID</c> already exists in the database, it will be
        /// updated with the values from <paramref name="ii__style"/>. Otherwise, a new style record will be created and
        /// added to the database.</remarks>
        /// <param name="ii__style">The style entity to save. Must have a valid <c>styleID</c> for updates.</param>
        internal static void SaveRawStyle(Web.Data.Models.BlankStyle ii__style)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var existingEntity = context.Styles.Find(ii__style.styleID); // Find by primary key

                    if (existingEntity != null)
                    {
                        // Update existing entity
                        existingEntity.partNumber = ii__style.partNumber;
                        existingEntity.brandName = ii__style.brandName;
                        existingEntity.styleName = ii__style.styleName;
                        existingEntity.uniqueStyleName = ii__style.uniqueStyleName;
                        existingEntity.title = ii__style.title;
                        existingEntity.description = ii__style.description;
                        existingEntity.baseCategory = ii__style.baseCategory;
                        existingEntity.categories = ii__style.categories;
                        existingEntity.catalogPageNumber = ii__style.catalogPageNumber;
                        existingEntity.newStyle = ii__style.newStyle;
                        existingEntity.comparableGroup = ii__style.comparableGroup;
                        existingEntity.companionGroup = ii__style.companionGroup;
                        existingEntity.brandImage = ii__style.brandImage;
                        existingEntity.styleImage = ii__style.styleImage;
                        existingEntity.noeRetailing = ii__style.noeRetailing;
                        existingEntity.boxRequired = ii__style.boxRequired;
                        existingEntity.dateLastChanged = ii__style.dateLastChanged;
                        context.Styles.Update(existingEntity);
                    
                }
                    else
                    {
                        // Create new entity

                        context.Styles.Add(ii__style);
                    }
                    context.SaveChanges(); // EF determines whether to INSERT or UPDATE
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error saving style: " + ex.Message + ex.InnerException);
            }

        }

        /// <summary>
        /// Saves the specified product to the database. If a product with the same primary key already exists, it is
        /// updated; otherwise, a new product is added.
        /// </summary>
        /// <remarks>This method uses Entity Framework to manage the persistence of the product. The
        /// operation is performed within a database context,  and changes are committed to the database. If an
        /// exception occurs during the operation, it is logged to the console.</remarks>
        /// <param name="ii__product">The product to save, containing the data to be inserted or updated in the database.</param>
        internal static void SaveRawProducts(Web.Data.Models.BlankProduct ii__product)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var existingEntity = context.Products.Find(ii__product.skuID_Master); 
                    // Find by primary key
                    //var myent = existingEntity.FirstOrDefault();
                    if (existingEntity != null)
                    {
                        // Console.WriteLine(existingEntity.ToString());
                        // Update existing entity
                        existingEntity.sku = ii__product.sku;
                        existingEntity.gtin = ii__product.gtin;
                        existingEntity.yourSku = ii__product.yourSku;
                        existingEntity.styleID = ii__product.styleID;
                        existingEntity.brandName = ii__product.brandName;
                        existingEntity.styleName = ii__product.styleName;   
                        existingEntity.colorName = ii__product.colorName;   
                        existingEntity.colorCode = ii__product.colorCode;   
                        existingEntity.colorPriceCodeName = ii__product.colorPriceCodeName;
                        existingEntity.colorGroup = ii__product.colorGroup;                            
                        existingEntity.colorGroupName = ii__product.colorGroupName;
                        existingEntity.colorFamilyID = ii__product.colorFamilyID;
                        existingEntity.colorSwatchImage = ii__product.colorSwatchImage;
                        existingEntity.colorSwatchTextColor = ii__product.colorSwatchTextColor;
                        existingEntity.colorFrontImage = ii__product.colorFrontImage;
                        existingEntity.colorSideImage = ii__product.colorSideImage;
                        existingEntity.colorBackImage = ii__product.colorBackImage;
                        existingEntity.colorDirectSideImage = ii__product.colorDirectSideImage;
                        existingEntity.colorOnModelFrontImage = ii__product.colorOnModelFrontImage;
                        existingEntity.colorOnModelSideImage = ii__product.colorOnModelSideImage;
                        existingEntity.colorOnModelBackImage = ii__product.colorOnModelBackImage;
                        existingEntity.color1 = ii__product.color1;
                        existingEntity.color2 = ii__product.color2;
                        existingEntity.sizeName = ii__product.sizeName;
                        existingEntity.sizeCode = ii__product.sizeCode;
                        existingEntity.sizeOrder = ii__product.sizeOrder;
                        existingEntity.sizePriceCodeName = ii__product.sizePriceCodeName;
                        existingEntity.caseQty = ii__product.caseQty;
                        existingEntity.unitWeight = ii__product.unitWeight;
                        existingEntity.mapPrice = ii__product.mapPrice;
                        existingEntity.piecePrice = ii__product.piecePrice;
                        existingEntity.dozenPrice = ii__product.dozenPrice;
                        existingEntity.casePrice = ii__product.casePrice;
                        existingEntity.salePrice = ii__product.salePrice;
                        existingEntity.customerPrice = ii__product.customerPrice;
                        existingEntity.saleExpiration = ii__product.saleExpiration;
                        existingEntity.noeRetailing = ii__product.noeRetailing;
                        existingEntity.caseWeight = ii__product.caseWeight;
                        existingEntity.caseWidth = ii__product.caseWidth;
                        existingEntity.caseLength = ii__product.caseLength;
                        existingEntity.caseHeight = ii__product.caseHeight;
                        existingEntity.qty = ii__product.qty;
                        existingEntity.countryOfOrigin = ii__product.countryOfOrigin;
                        existingEntity.dateLastChanged =  ii__product.dateLastChanged;
                        context.Products.Update(existingEntity);                            
                    }
                    else
                    {
                        // Create new entity
                        context.Products.Add(ii__product);
                    }
                    context.SaveChanges(); // EF determines whether to INSERT or UPDATE
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving product: " + ex.Message);
            }

        }


        /// <summary>
        /// Saves the specified style to the database, either by updating an existing record or adding a new one.
        /// </summary>
        /// <remarks>If a style with the same <c>styleID</c> already exists in the database, it will be
        /// updated with the values from <paramref name="ii__category"/>. Otherwise, a new style record will be created and
        /// added to the database.</remarks>
        /// <param name="ii__category">The style entity to save. Must have a valid <c>styleID</c> for updates.</param>
        internal static void SaveRawCategory(Web.Data.Models.BlankCategory ii__category)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var existingEntity = context.Categories.Find(ii__category.categoryID); // Find by primary key

                    if (existingEntity != null)
                    {
                        // Update existing entity
                        existingEntity.name = ii__category.name;

                        existingEntity.image = ii__category.image;
                        context.Categories.Update(existingEntity);

                    }
                    else
                    {
                        // Create new entity

                        context.Categories.Add(ii__category);
                    }
                    context.SaveChanges(); // EF determines whether to INSERT or UPDATE
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error saving style: " + ex.Message + ex.InnerException);
            }

        }


        internal static void SaveRawSanMar(Web.Data.Models.BlankSanMar ii__sanmar)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var existingEntity = context.SanMars.Find(ii__sanmar.UNIQUE_KEY); // Find by primary key

                    if (existingEntity != null)
                    {
                        // Update existing entity
                        existingEntity.PRODUCT_TITLE = ii__sanmar.PRODUCT_TITLE;
                        existingEntity.PRODUCT_DESCRIPTION = ii__sanmar.PRODUCT_DESCRIPTION;
                        existingEntity.STYLE = ii__sanmar.STYLE;
                        existingEntity.AVAILABLE_SIZES = ii__sanmar.AVAILABLE_SIZES;
                        existingEntity.BRAND_LOGO_IMAGE = ii__sanmar.BRAND_LOGO_IMAGE;
                        existingEntity.THUMBNAIL_IMAGE = ii__sanmar.THUMBNAIL_IMAGE;
                        existingEntity.COLOR_SWATCH_IMAGE = ii__sanmar.COLOR_SWATCH_IMAGE;
                        existingEntity.PRODUCT_IMAGE = ii__sanmar.PRODUCT_IMAGE;
                        existingEntity.SPEC_SHEET = ii__sanmar.SPEC_SHEET;
                        existingEntity.PRICE_TEXT = ii__sanmar.PRICE_TEXT;
                        existingEntity.SUGGESTED_PRICE = ii__sanmar.SUGGESTED_PRICE;
                        existingEntity.CATEGORY_NAME = ii__sanmar.CATEGORY_NAME;
                        existingEntity.SUBCATEGORY_NAME = ii__sanmar.SUBCATEGORY_NAME;
                        existingEntity.COLOR_NAME = ii__sanmar.COLOR_NAME;
                        existingEntity.COLOR_SQUARE_IMAGE = ii__sanmar.COLOR_SQUARE_IMAGE;
                        existingEntity.COLOR_PRODUCT_IMAGE = ii__sanmar.COLOR_PRODUCT_IMAGE;
                        existingEntity.COLOR_PRODUCT_IMAGE_THUMBNAIL = ii__sanmar.COLOR_PRODUCT_IMAGE_THUMBNAIL;
                        existingEntity.SIZE = ii__sanmar.SIZE;
                        existingEntity.SIZE_SORT = ii__sanmar.SIZE_SORT;

                      existingEntity.PIECE_WEIGHT = ii__sanmar.PIECE_WEIGHT;


                        existingEntity.dateLastChanged = ii__sanmar.dateLastChanged;


                        context.SanMars.Update(existingEntity);

                    }
                    else
                    {
                        // Create new entity

                        context.SanMars.Add(ii__sanmar);
                    }
                    context.SaveChanges(); // EF determines whether to INSERT or UPDATE
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error saving sanmar item: " + ex.Message + ex.InnerException);
            }
        }
    }
}
