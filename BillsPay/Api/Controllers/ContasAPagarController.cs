using Domain.DTO.Requests;
using Domain.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasAPagarController : ControllerBase
    {
        private ContasAPagarService ContasAPagarService { get; set; }

        public ContasAPagarController(ContasAPagarService contasAPagarService)
        {
            ContasAPagarService = contasAPagarService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await ContasAPagarService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContaAPagarRequest request)
        {
            var result = await ContasAPagarService.Create(request);
            return Ok(result);
        }
    }
}
