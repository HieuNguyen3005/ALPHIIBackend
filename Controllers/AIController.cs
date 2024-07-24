using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using ALPHII.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ALPHII.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using ALPHII.Data;
using ALPHII.Repositories;

namespace ALPHII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IAIRepository _aIRepository;
        public AIController( IAIRepository aIRepository)
        {
            _aIRepository = aIRepository;
        }

        [HttpPost]
        [Route("VirtualTryOn")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> VirtualTryOn([FromBody] VirtualTryOnRequestDto virtualTryOnRequestDto)
        {

            var virtualTryOnResult = _aIRepository.VirtualTryOnAsync(virtualTryOnRequestDto);
            if(virtualTryOnResult == null)
            {
                return BadRequest();
            }
            return Ok(virtualTryOnResult);
        }

        [HttpPost]
        [Route("VirtualModel/GetSegment")]
        public async Task<IActionResult> GetSegment([FromBody] GetSegmentRequestDto getSegmentRequestDto)
        {
            var base_image_base64 = Common.FunctionCommon.ConvertImageToBase64(getSegmentRequestDto.base_image_url);
            var getSegmentResult = await _aIRepository.GetSegmentAsync(base_image_base64);
            if(getSegmentResult == null)
            {
                return BadRequest();
            }    
            return Ok(getSegmentResult);
        }

        [HttpPost]
        [Route("VirtualModel")]
        public async Task<IActionResult> VirtualModel([FromBody] VirtualModelRequestDto virtualModelRequestDto)
        {
            var VirtualModelResponseDto = await _aIRepository.VirtualModelAsync(virtualModelRequestDto);
            if(VirtualModelResponseDto == null)
            {
                return BadRequest();
            }

            return Ok(VirtualModelResponseDto);
        }


    }
}
