using ALPHII.Data;
using ALPHII.Models.Domain;
namespace ALPHII.Repositories
{
    public class LocalProjectRepository : IProjectRepository
    {
        private readonly ALPHIIBackendDbContext _dbContext;
        public LocalProjectRepository(ALPHIIBackendDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Project>> GetAllProjectByToolIdAsync(string ToolId)
        {
            return await _dbContext.Projects.FindByIdAsync(ToolId);
        }

        public async Task<Project> GetVmProjectById(strong VmProjectId)
        {
            
        }

        public async Task<Project> CreateProjectAsync(Image image)
        {
            
        }

        public async Task<Project> UpdateProjectAsync(Image image)
        {

        }

        public async Task<Project> DeleteProjectAsync(Image image)
        {

        }


    }

}