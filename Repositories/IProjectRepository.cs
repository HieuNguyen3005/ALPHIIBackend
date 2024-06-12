using ALPHII.Models.Domain;

namespace ALPHII.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllProjectByToolIdAsync();

        Task<Project> GetVmProjectByIdAsync(strong VmProjectId);

        Task<Project> CreateProjectAsync();

        Task<Project> UpdateProjectAsync();

        Task<Project> DeleteProjectAsync();

        
    }
}
