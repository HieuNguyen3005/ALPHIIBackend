using ALPHII.Data;
using ALPHII.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ALPHII.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ALPHIIBackendDbContext dbContext;
        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ALPHIIBackendDbContext dbContext) {
        
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext; 
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            // Upload Image to Local 
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // https://localhost:1234/images/image.jpg

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{httpContextAccessor.HttpContext.Request.Host}:" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            // Add image to the Images table
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();


            return image;
        }

        public async Task<string> SaveImage(string imageInBase64)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "images/virtualTryOnResult.jpg");
            byte[] imageBytes = Convert.FromBase64String(imageInBase64);
            // Write byte array to file
            File.WriteAllBytes(localFilePath, imageBytes);


            var image = new Image
            {
                FileExtension = "jpg",
                FileSizeInBytes = imageBytes.Length,
                FileName = "virtualTryOnResult",
                FileDescription = "Virtual Try On Result",
                Type = 3
            };
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{httpContextAccessor.HttpContext.Request.Host}:" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;
            // Save image to database
            // Add image to the Images table
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return urlFilePath;
        }

        public async Task<List<Image>> GetAllImages()
        {
            return await dbContext.Images.ToListAsync();
        }
    }
}
