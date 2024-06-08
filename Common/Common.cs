using ALPHII.Models.Domain;
using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace ALPHII.Common
{
    public class FunctionCommon
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public static string ConvertImageToBase64(string imagePath)
        {
            try
            {
                var parts = imagePath.Split('/');
                var fileName = parts[parts.Length-1];
                byte[] imageBytes = File.ReadAllBytes("images/"+fileName);

                string base64Image = Convert.ToBase64String(imageBytes);
                

                return base64Image; 
            }

            catch(Exception ex)
            {
                Console.WriteLine("Error converting image to base64" + ex.Message);
                return null;
            }
        }

        //public static Image ConvertBase64ToImage(string base64String)
        //{
        //    IWebHostEnvironment webHostEnvironment = new WebHostEnvironment();
        //    try
        //    {
        //        string outputFileName = "https://localhost:7238:/images/output_image.jpg";
        //        var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
        //        // Convert Base64 string to byte array
        //        byte[] imageBytes = Convert.FromBase64String(base64String);

        //        // Write byte array to file
        //        File.WriteAllBytes(outputFileName, imageBytes);

        //        var imageDomainModel = new Image
        //        {
        //            FileExtension = "jpg",
        //            FileSizeInBytes = imageBytes.Length,
        //            FileName = "output_image",
        //            FileDescription = "Virtual Try On Result",
        //            Type = 3
        //        };
        //        return imageDomainModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //        Console.WriteLine("Error: " + ex.Message);
        //    }
        //}
    }
}
