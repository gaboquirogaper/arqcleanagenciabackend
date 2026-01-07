using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        // GET: api/clientes/5 (Para editar necesitamos buscar uno solo primero)
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var cliente = await _repository.ObtenerPorId(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<IActionResult> Crear(Cliente cliente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _repository.Crear(cliente);
            return CreatedAtAction(nameof(ObtenerTodos), new { id = cliente.Id }, cliente);
        }

        // PUT: api/clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest("El ID de la URL no coincide.");

            var existente = await _repository.ObtenerPorId(id);
            if (existente == null) return NotFound("Cliente no encontrado.");

            // Actualizamos los campos
            existente.Nombre = cliente.Nombre;
            existente.Apellido = cliente.Apellido;
            existente.Email = cliente.Email;
            existente.Telefono = cliente.Telefono;

            await _repository.Actualizar(existente);
            return NoContent();
        }

        // DELETE: api/clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existente = await _repository.ObtenerPorId(id);
            if (existente == null) return NotFound("Cliente no encontrado.");

            await _repository.Eliminar(id);
            return NoContent();
        }
    }
}