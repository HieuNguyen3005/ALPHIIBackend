using System.ComponentModel.DataAnnotations;

namespace ALPHII.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }

        public int Type { get; set; }
    }
}
