using ALPHII.Models.Domain;
using ALPHII.Models.DTO;

namespace ALPHII.Repositories
{
    public interface IToolRepository
    {
        public Task<List<Tool>> GetAllToolAsync();
        public Task<Tool> CreateToolAsync(Tool tool);
        public Task<Tool?> GetToolByIdAsync(Guid id);
        public Task<Tool?> UpdateToolAsync(Guid id, UpdateToolRequestDto updateToolRequestDto);
        public Task<Tool?> DeleteToolAsync(Guid id);
    }
}
