using Domain.DTO.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasAPagarController : ControllerBase
    {
        private ContasAPagarService Service { get; set; }

        public ContasAPagarController(ContasAPagarService service)
        {
            Service = service;
        }       
    }
}
