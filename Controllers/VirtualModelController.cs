using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ALPHII.Common;
using ALPHII.Models.DTO;

namespace ALPHII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VirtualModelController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ALPHIIBackendDbContext _dbContext;

        public VirtualModelController(IHttpClientFactory httpClientFactory, ALPHIIBackendDbContext dbContext)
        {
          this._httpClientFactory = httpClientFactory;
          this._dbContext = dbContext;
        }

        [HttpPost]
        [Route("GetSegment")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetSegment([FromBody] VirtualModelRequestDto virtualModelRequestDto)
        {
            
        }
        
    }


}