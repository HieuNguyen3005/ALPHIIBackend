using ALPHII.Models.Domain;
using ALPHII.Models.DTO;

namespace ALPHII.Repositories
{
    public interface IWalkRepository
    {
        public Task<List<Walk>> GetAllWalkAsync(int pageNumber, int pageSize, string? filterOn, string? filterQuery, string? sortBy, bool isAscending);
        public Task<Walk> CreateAsync(Walk walk);
        public Task<Walk?> GetByIdAsync(Guid id);
        public Task<Walk?> UpdateAsync(Guid id, Walk walk);
        public Task<Walk?> DeleteAsync(Guid id);
    }
}
