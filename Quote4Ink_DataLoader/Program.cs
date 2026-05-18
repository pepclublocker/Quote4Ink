// See https://aka.ms/new-console-template for more information
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quote4Ink.Data;
using Web.Data.Models;
using System;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


class Program
{
    static async Task Main(string[] args)
    {

        var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);

        var config = builder.Build();

        // Run the get of catalog data
        string ResultStyle = string.Empty;
        Stopwatch stopwatch = new Stopwatch();
        string Messages = string.Empty;


        //string LogDirectory = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString();
        string PathDirectory = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString();

        try
        {
            Messages = "\n";
            Directory.CreateDirectory(PathDirectory);
            Messages += "Creating directory for catalog data: " + PathDirectory + "\n";
        }
        catch (Exception ex)
        {
            Messages += "Creating directory for catalog data: " + PathDirectory + "\n";
            Messages += "Error creating directory: " + ex.Message + "\n";
            Messages += "Inner Exception: " + ex.InnerException + "\n";
        }
        finally
        {
            File.AppendAllText(PathDirectory + "/Log.txt", Messages);
        }

        //download all relvant files from the SS Activewear API
        using (Quote4Ink.Service.Functions GetFuncs = new())
        {
            await GetFuncs.GetFile2("products", PathDirectory);
            await GetFuncs.GetFile2("styles", PathDirectory);
            await GetFuncs.GetFile2("categories", PathDirectory);
            await GetFuncs.GetFile2("specs", PathDirectory);

            await GetFuncs.GetSanMar(PathDirectory);
        }



        GC.Collect();
        GC.WaitForPendingFinalizers();



        //get all products from the SS Activewear API
        using (var stream = File.OpenRead(PathDirectory + "/products.json"))
        using (var reader = new StreamReader(stream))
        using (var jsonReader = new JsonTextReader(reader))
        {
            var serializer = new Newtonsoft.Json.JsonSerializer();

            // Move to the start of the array
            if (jsonReader.Read() && jsonReader.TokenType == JsonToken.StartArray)
            {
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType == JsonToken.StartObject)
                    {
                        var product = serializer.Deserialize<Web.Data.Models.BlankProduct>(jsonReader);
                        SQLRepository.SaveRawProducts(product);
                    }
                    else if (jsonReader.TokenType == JsonToken.EndArray)
                    {
                        break;
                    }
                }
            }
        }

        //get all styles from the SS Activewear API
        using (var stream = File.OpenRead(PathDirectory + "/styles.json"))
        using (var reader = new StreamReader(stream))
        using (var jsonReader = new JsonTextReader(reader))
        {
            var serializer = new Newtonsoft.Json.JsonSerializer();

            // Move to the start of the array
            if (jsonReader.Read() && jsonReader.TokenType == JsonToken.StartArray)
            {
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType == JsonToken.StartObject)
                    {
                        var style = serializer.Deserialize<Web.Data.Models.BlankStyle>(jsonReader);
                        SQLRepository.SaveRawStyle(style);
                    }
                    else if (jsonReader.TokenType == JsonToken.EndArray)
                    {
                        break;
                    }
                }
            }
        }

        ////get all styles from the SS Activewear API
        //using (var stream = File.OpenRead(PathDirectory + "/categories.json"))
        //using (var reader = new StreamReader(stream))
        //using (var jsonReader = new JsonTextReader(reader))
        //{
        //    var serializer = new JsonSerializer();

        //    // Move to the start of the array
        //    if (jsonReader.Read() && jsonReader.TokenType == JsonToken.StartArray)
        //    {
        //        while (jsonReader.Read())
        //        {
        //            if (jsonReader.TokenType == JsonToken.StartObject)
        //            {
        //                var category = serializer.Deserialize<S_S_Category>(jsonReader);
        //                SQLRepository.SaveRawCategory(category);
        //            }
        //            else if (jsonReader.TokenType == JsonToken.EndArray)
        //            {
        //                break;
        //            }
        //        }
        //    }
        //}

        var configCSV = new CsvConfiguration(CultureInfo.InvariantCulture);

        configCSV.HasHeaderRecord = true;
        configCSV.ReadingExceptionOccurred = re =>
        {
            Debug.WriteLine($"Bad Row in file; CSV ERROR: {re.Exception}");
            return false;
        };

        using (var reader = new StreamReader(PathDirectory + "/Sanmar_SDL_N.csv"))
        using (var csv = new CsvReader(reader, configCSV))
        {
            csv.Context.RegisterClassMap<MapSanMar>();
            foreach (var record in csv.GetRecords<Web.Data.Models.BlankSanMar>())
            {
                SQLRepository.SaveRawSanMar(record);
            }

            //_repRepository.SaveRawSanMar(records);
            //save records
        }
    }


    public sealed class MapSanMar : ClassMap<Web.Data.Models.BlankSanMar>
    {
        public MapSanMar()
        {
            Map(m => m.UNIQUE_KEY).Name("UNIQUE_KEY");
            Map(m => m.PRODUCT_TITLE).Name("PRODUCT_TITLE");
            Map(m => m.PRODUCT_DESCRIPTION).Name("PRODUCT_DESCRIPTION");
            Map(m => m.STYLE).Name("STYLE#");
            Map(m => m.AVAILABLE_SIZES).Name("AVAILABLE_SIZES");
            Map(m => m.BRAND_LOGO_IMAGE).Name("BRAND_LOGO_IMAGE");
            Map(m => m.THUMBNAIL_IMAGE).Name("THUMBNAIL_IMAGE");
            Map(m => m.COLOR_SWATCH_IMAGE).Name("COLOR_SWATCH_IMAGE");
            Map(m => m.PRODUCT_IMAGE).Name("PRODUCT_IMAGE");
            Map(m => m.SPEC_SHEET).Name("SPEC_SHEET");
            Map(m => m.PRICE_TEXT).Name("PRICE_TEXT");
            Map(m => m.SUGGESTED_PRICE).Name("SUGGESTED_PRICE");
            Map(m => m.CATEGORY_NAME).Name("CATEGORY_NAME");
            Map(m => m.SUBCATEGORY_NAME).Name("SUBCATEGORY_NAME");
            Map(m => m.COLOR_NAME).Name("COLOR_NAME");
            Map(m => m.COLOR_SQUARE_IMAGE).Name("COLOR_SQUARE_IMAGE");
            Map(m => m.COLOR_PRODUCT_IMAGE).Name("COLOR_PRODUCT_IMAGE");
            Map(m => m.COLOR_PRODUCT_IMAGE_THUMBNAIL).Name("COLOR_PRODUCT_IMAGE_THUMBNAIL");
            Map(m => m.SIZE).Name("SIZE");
            Map(m => m.PIECE_WEIGHT).Name("PIECE_WEIGHT");
            Map(m => m.PIECE_PRICE).Name("PIECE_PRICE");
            Map(m => m.DOZENS_PRICE).Name("DOZENS_PRICE");
            Map(m => m.CASE_PRICE).Name("CASE_PRICE");
            Map(m => m.PRICE_GROUP).Name("PRICE_GROUP");
            Map(m => m.CASE_SIZE).Name("CASE_SIZE");
            Map(m => m.INVENTORY_KEY).Name("INVENTORY_KEY");
            Map(m => m.SIZE_INDEX).Name("SIZE_INDEX");
            Map(m => m.SANMAR_MAINFRAME_COLOR).Name("SANMAR_MAINFRAME_COLOR");
            Map(m => m.MILL).Name("MILL");
            Map(m => m.PRODUCT_STATUS).Name("PRODUCT_STATUS");
            Map(m => m.COMPANION_STYLE).Name("COMPANION_STYLE");
            // Map(m => m.MSRP).Name("MSRP");
            Map(m => m.MAP_PRICING).Name("MAP_PRICING");
            Map(m => m.FRONT_MODEL_IMAGE_URL).Name("FRONT_MODEL_IMAGE_URL");
            Map(m => m.BACK_MODEL_IMAGE_URL).Name("BACK_MODEL_IMAGE_URL");
            Map(m => m.FRONT_FLAT_IMAGE_URL).Name("FRONT_FLAT_IMAGE_URL");
            Map(m => m.BACK_FLAT_IMAGE_URL).Name("BACK_FLAT_IMAGE_URL");
            Map(m => m.PRODUCT_MEASUREMENTS).Name("PRODUCT_MEASUREMENTS");
            Map(m => m.PMS_COLOR).Name("PMS_COLOR");
            Map(m => m.GTIN).Name("GTIN");
            Map(m => m.DECORATION_SPEC_SHEET).Name("DECORATION_SPEC_SHEET");

        }
    }
}







