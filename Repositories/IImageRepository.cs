using ALPHII.Models.Domain;

namespace ALPHII.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);

        Task<List<Image>> GetAllImages();

        Task<string> SaveImage(string imageInBase64);
    }
}
