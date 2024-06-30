using ALPHII.Models.Domain;
using ALPHII.Models.DTO;
using ALPHII.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALPHII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;

        public ProjectController(IProjectRepository projectRepository, IMapper mapper)
        {
            this.projectRepository = projectRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllProject")]
        public async Task<IActionResult> GetAllProjectByToolIdAndUserId([FromBody] GetAllProjectRequest getProjectRequest)
        {
            var projectDomainModels = await projectRepository.GetAllProjectByToolIdAndUserIdAsync(getProjectRequest.ToolId, getProjectRequest.UserId);
            return Ok(mapper.Map<List<ProjectDto>>(projectDomainModels));
        }

        [HttpGet]
        [Route("GetVMProject")]
        public async Task<IActionResult> GetVMProjectByToolIdAndUserIdAndProjectId([FromBody] GetVmProjectRequest getVmProjectRequest)
        {
            var projectDomain = await projectRepository.GetVMProjectByToolIdAndUserIdAndProjectIdAsync(getVmProjectRequest.ToolId, getVmProjectRequest.UserId, getVmProjectRequest.ProjectId);
            return Ok(mapper.Map<List<ProjectDto>>(projectDomain));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectRequest createProjectRequest)
        {
            var projectDomain = await projectRepository.CreateProjectAsync(createProjectRequest.ToolId, createProjectRequest.UserId);
            return Ok(mapper.Map<ProjectDto>(projectDomain));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateVmProject([FromRoute] Guid id, [FromBody] UpdateVmProjectRequestDto updateVmProjectRequestDto)
        {
            var projectDomainModel = mapper.Map<Project>(updateVmProjectRequestDto);

            projectDomainModel = await projectRepository.UpdateVmProjectAsync(id, projectDomainModel);

            if (projectDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProjectDto>(projectDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProject([FromRoute] Guid id) 
        {
            var projectDomainModel = await projectRepository.DeleteProjectAsync(id);

            if (projectDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProjectDto>(projectDomainModel));
        }
    }
}
