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
    public class PlanController : ControllerBase
    {
        private readonly IPlanRepository PlanRepository;
        private readonly IMapper mapper;
        public PlanController(IPlanRepository PlanRepository, IMapper mapper)
        {
            this.PlanRepository = PlanRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlan()
        {
            var PlanDomainModels = await PlanRepository.GetAllPlanAsync();
            return Ok(mapper.Map<List<PlanDto>>(PlanDomainModels));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] AddPlanRequestDto addPlanRequestDto)
        {
                var PlanDomainModel = mapper.Map<Plan>(addPlanRequestDto);
                await PlanRepository.CreatePlanAsync(PlanDomainModel);
                return Ok(mapper.Map<PlanDto>(PlanDomainModel));

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPlanById([FromRoute] Guid id)
        {
           var PlanDomainModel = await PlanRepository.GetPlanByIdAsync(id);
           if(PlanDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PlanDto>(PlanDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody]  UpdatePlanRequestDto updatePlanRequestDto)
        {
            var planDomainModel = mapper.Map<Plan>(updatePlanRequestDto);

            planDomainModel = await PlanRepository.UpdatePlanAsync(id, planDomainModel);
            if(planDomainModel == null)
            {
                return NotFound();
            }    
            return Ok(mapper.Map<PlanDto>(planDomainModel));   
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var PlanDomainModel = await PlanRepository.DeletePlanAsync(id);
            if (PlanDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PlanDto>(PlanDomainModel));

        }
    }
}
