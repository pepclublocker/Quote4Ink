using Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace Quote4Ink.Service
{
    public class Functions : IDisposable
    {
        public void Dispose()
        {
            // Implement any necessary cleanup here if needed
            GC.SuppressFinalize(this);
        }
        ~Functions()
        {
            Dispose();
        }

        public async Task<string> GetFile(string urlType)
        {
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(config["SandS:userid"].ToString() + ":" + config["SandS:apikey"].ToString())));
                    client.Timeout = TimeSpan.FromMinutes(30); // Set timeout to 30 minutes

                    HttpResponseMessage response = await client.GetAsync("https://api.ssactivewear.com/v2/" + urlType);

                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading configuration: " + ex.Message);
                throw;
            }
        }

        public string ReadableTime(TimeSpan t)
        {
            string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds);
            return answer;
        }

        public async Task GetFile2(string urlType, string filepath)
        {
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();
            try
            {
                Console.WriteLine("File Time start: " + urlType + " " + DateTime.Now.ToString());
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(config["SandS:userid"].ToString() + ":" + config["SandS:apikey"].ToString())));
                    client.Timeout = TimeSpan.FromMinutes(30); // Set timeout to 30 minutes\



                    // Initiate the HTTP GET request. HttpCompletionOption.ResponseHeadersRead
                    // ensures that the HttpClient only reads the response headers and
                    // doesn't buffer the entire response content into memory.
                    var response = await client.GetAsync("https://api.ssactivewear.com/v2/" + urlType, HttpCompletionOption.ResponseHeadersRead);

                    response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response indicates an error

                    // Obtain the response content as a stream
                    await using var contentStream = await response.Content.ReadAsStreamAsync();

                    // Create a FileStream to write the downloaded data to disk
                    await using var fileStream = new FileStream(filepath + "/" + urlType + ".json", FileMode.Create, FileAccess.Write, FileShare.None);

                    // Copy the content stream (received in chunks) directly to the file stream
                    await contentStream.CopyToAsync(fileStream);

                    //Console.WriteLine($"Downloaded and saved: {destinationPath}");
                }
                Console.WriteLine("File Time end: " + urlType + " " + DateTime.Now.ToString());
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error: {ex.Message}"); //
                                                                // Handle HTTP-related errors (e.g., network issues, invalid URL)
            }
            catch (IOException ex)
            {
                Console.WriteLine($"File I/O error: {ex.Message}"); //
                                                                    // Handle file-related errors (e.g., permission issues, disk space)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                // Handle other potential errors
            }
        }

        public async Task GetSanMar(string filepath)
        {
            using (Stream streamToWriteTo = System.IO.File.Open(filepath + "/Sanmar_SDL_N.csv", FileMode.Create))
            {
                var client = new SftpClient("ftp.sanmar.com", 2200, "199921", "YsF6eCKTaisgC4");

                client.Connect();

                
                client.DownloadFile("/SanMarPDD/SanMar_SDL_N.csv", streamToWriteTo);
            }
        }
    }
}
