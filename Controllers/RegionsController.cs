using ALPHII.CustomActionFilters;
using ALPHII.Data;
using ALPHII.Models.Domain;
using ALPHII.Models.DTO;
using ALPHII.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;

namespace AiphiiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: /api/walks?filterOn=Name&filterQuery=Track
        [HttpGet]
        public async Task<IActionResult> getAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {

            // Get Region Domain From Database
            var regionDomainModels = await regionRepository.GetAllAsync(filterOn,filterQuery);

            //Map Domain Models to DTOs
            // Return DTOs
            logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionDomainModels)}");
            return Ok(mapper.Map<List<RegionDto>>(regionDomainModels));
        }

        // Get single region(Get region by id)
        // Url: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Region Domain From Database
            var regionDomainModel = await regionRepository.GetByIdAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        //POST To Create New Region
        //POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
                // Map or convert DTO to Domain Model
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                // Use Domain Model to create Region
                await regionRepository.CreateAsync(regionDomainModel);

                // Map Domain model back to DTO
                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, mapper.Map<RegionDto>(regionDomainModel));
        }

        //Put: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Map or convert DTO to Domain Model
            var region = mapper.Map<Region>(updateRegionRequestDto);

            // Use Domain Model to update region
            var regionDomainModel = await regionRepository.UpdateAsync(id,region);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Map or convert Domain Model to Dto
            return Ok(mapper.Map<RegionDto>(regionDomainModel)); 
        }

        //Delete: https://localhost:portnumber/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Return deleted Region back
            // map Domain Model to DTO
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
