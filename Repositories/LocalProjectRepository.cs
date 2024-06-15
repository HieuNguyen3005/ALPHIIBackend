using ALPHII.Data;
using ALPHII.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace ALPHII.Repositories
{
    public class LocalProjectRepository : IProjectRepository
    {
        private readonly ALPHIIBackendDbContext _dbContext;
        public LocalProjectRepository(ALPHIIBackendDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Project>> GetAllProjectByToolIdAndUserIdAsync(Guid ToolId, Guid UserId)
        {
            return await _dbContext.Projects.Where(p => p.ToolId == ToolId && p.UserId == UserId).ToListAsync();
        }

        public async Task<Project> GetVMProjectByToolIdAndUserIdAndProjectIdAsync(Guid ToolId, Guid UserId, Guid ProjectId)
        {
            var existionProject = await _dbContext.Projects.Include(x => x.VMProject).FirstOrDefaultAsync(p => p.ToolId == ToolId && p.UserId == UserId && p.Id == ProjectId);
            if (existionProject == null)
            {
                return null;
            }
            return existionProject;
        }

        public async Task<Project> CreateProjectAsync(Guid ToolId, Guid UserId)
        {
            Random random = new Random();
            string randomNumber = "";
            for(int i = 0; i < 8; i++)
            {
                randomNumber += random.Next(0, 10).ToString();
            }

             Project newProject = new Project
             {
                 ToolId = ToolId,
             
                 UserId = UserId,
             
                 ProjectName = "Task-" + randomNumber,
             
                 Note = null,
                 //  State: 0 - Not Started; 1 - In processing; 2 - completed
                 State = 0
             };
             await _dbContext.Projects.AddAsync(newProject);
             await _dbContext.SaveChangesAsync();
             
             return newProject;
        }

        public async Task<Project> UpdateVmProjectAsync(Guid ProjectId, Project project)
        {
                var existionProject = await _dbContext.Projects.Include(x => x.VMProject).FirstOrDefaultAsync(x => x.Id == ProjectId);
                if (existionProject == null)
                {
                    return null;
                }
                existionProject.ProjectName = project.ProjectName;
                existionProject.State = project.State;
                existionProject.Note = project.Note;
                existionProject.VMProject = project.VMProject;
                await _dbContext.SaveChangesAsync();
                return existionProject;
        }

        public async Task<Project> DeleteProjectAsync(Guid ProjectId)
        {
            var existionProject = await _dbContext.Projects.Include(x => x.VMProject).FirstOrDefaultAsync(x => x.Id == ProjectId);
            if (existionProject == null)
            {
                return null;
            }
            _dbContext.Remove(existionProject);
            await _dbContext.SaveChangesAsync();
            return existionProject;
        }
    }

}