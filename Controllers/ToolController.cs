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
        private readonly IToolRepository toolRepository;
        private readonly IMapper mapper;
        public ToolController(IToolRepository toolRepository, IMapper mapper)
        {
            this.toolRepository = toolRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTool([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAsending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var walkDomainModels = await walkRepository.GetAllWalkAsync(pageNumber, pageSize, filterOn, filterQuery, sortBy, isAsending ?? true);
            throw new Exception("This is a new exception");
            return Ok(mapper.Map<List<WalkDto>>(walkDomainModels));
        }
    }
}
