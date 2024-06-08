using ALPHII.Models.Domain;
using ALPHII.Models.DTO;

namespace ALPHII.Repositories
{
    public interface IRegionRepository
    {
        public Task<List<Region>> GetAllAsync(string? filterOn, string? filterQuery);

        public Task<Region?> GetByIdAsync(Guid id);

        public Task<Region> CreateAsync(Region region);

        public Task<Region?> UpdateAsync(Guid id, Region region);

        public Task<Region?> DeleteAsync(Guid id);

    }
}
