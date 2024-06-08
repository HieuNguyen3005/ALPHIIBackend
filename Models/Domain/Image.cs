using System.ComponentModel.DataAnnotations.Schema;

namespace ALPHII.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        //exclued from database mapping
        [NotMapped]
        public IFormFile File { get; set; } 

        public string FileName { get; set; }

        public string? FileDescription { get; set; }

        public string FileExtension { get; set; } 

        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }

        /// <summary>
        /// 1: Model, 2 Cloth
        /// </summary>
        public int Type { get; set; }
    }
}
