using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _repository;

        public ClientesController(IClienteRepository repository)
        {
            _repository = repository;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var clientes = await _repository.ObtenerTodos();
            return Ok(clientes);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Cliente inválido");

            await _repository.Crear(cliente);
            return Ok(cliente);
        }
    }
}
