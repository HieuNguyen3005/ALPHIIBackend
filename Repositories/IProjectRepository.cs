using ALPHII.Models.Domain;

namespace ALPHII.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllProjectByToolIdAndUserIdAsync(Guid ToolId, Guid UserId);

        Task<Project> GetVMProjectByToolIdAndUserIdAndProjectIdAsync(Guid ToolId, Guid UserId, Guid ProjectId);

        Task<Project> CreateProjectAsync(Guid ToolId, Guid UserId);

        Task<Project> UpdateVmProjectAsync(Guid ProjectId, Project project);

        Task<Project> DeleteProjectAsync(Guid ProjectId);
    }
}
