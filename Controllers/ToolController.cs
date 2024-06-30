using ALPHII.CustomActionFilters;
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
    public class ToolController : ControllerBase
    {
        private readonly IToolRepository ToolRepository;
        private readonly IMapper mapper;
        public ToolController(IToolRepository ToolRepository, IMapper mapper)
        {
            this.ToolRepository = ToolRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTool()
        {
            var ToolDomainModels = await ToolRepository.GetAllToolAsync();
            return Ok(mapper.Map<List<ToolDto>>(ToolDomainModels));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTool([FromBody] AddToolRequestDto addToolRequestDto)
        {
                var ToolDomainModel = mapper.Map<Tool>(addToolRequestDto);
                await ToolRepository.CreateToolAsync(ToolDomainModel);
                return Ok(mapper.Map<ToolDto>(ToolDomainModel));

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetToolById([FromRoute] Guid id)
        {
           var ToolDomainModel = await ToolRepository.GetToolByIdAsync(id);
           if(ToolDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ToolDto>(ToolDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody]  UpdateToolRequestDto updateToolRequestDto)
        {
            var ToolDomainModel = await ToolRepository.UpdateToolAsync(id, updateToolRequestDto);
            if(ToolDomainModel == null)
            {
                return NotFound();
            }    
            return Ok(mapper.Map<ToolDto>(ToolDomainModel));   
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var ToolDomainModel = await ToolRepository.DeleteToolAsync(id);
            if (ToolDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ToolDto>(ToolDomainModel));

        }
    }
}
