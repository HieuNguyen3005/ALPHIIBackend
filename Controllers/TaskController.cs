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
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTaskByToolId()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask()
        {
            return Ok();
        }

        public
    }
}
