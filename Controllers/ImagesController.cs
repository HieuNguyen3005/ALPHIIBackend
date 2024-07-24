using ALPHII.Models.Domain;
using ALPHII.Models.DTO;
using ALPHII.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AiphiiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImagesController(IImageRepository imageRepository) { 
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UpLoad([FromForm]ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if(ModelState.IsValid)
            {

                // convert DTO to Domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    Type = request.Type
                };
                // User repository to upload image
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("GetAlls")]

        public async Task<IActionResult> GetAlls()
        {
            var listImage = await imageRepository.GetAllImages();
            return Ok(listImage);
        }


        private void ValidateFileUpload(ImageUploadRequestDto uploadRequest)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowedExtensions.Contains(Path.GetExtension(uploadRequest.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if(uploadRequest.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File sire more than 10MB, please upload a smaller size file");
            }

        }
    }
}
