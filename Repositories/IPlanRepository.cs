using ALPHII.Models.Domain;
using ALPHII.Models.DTO;

namespace ALPHII.Repositories
{
    public interface IPlanRepository
    {
        public Task<List<Plan>> GetAllPlanAsync();
        public Task<Plan> CreatePlanAsync(Plan Plan);
        public Task<Plan?> GetPlanByIdAsync(Guid id);

        public Task<Plan?> UpdatePlanAsync(Guid id, Plan Plan);
        public Task<Plan?> DeletePlanAsync(Guid id);
    }
}
