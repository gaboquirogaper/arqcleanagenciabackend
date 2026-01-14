using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Dashboard;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IObtenerDashboardUseCase _useCase;

        public DashboardController(IObtenerDashboardUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetResumen()
        {
            var stats = await _useCase.Execute();
            return Ok(stats);
        }
    }
}