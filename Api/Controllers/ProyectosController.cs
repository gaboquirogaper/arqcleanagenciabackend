using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly IProyectoRepository _repository;

        public ProyectosController(IProyectoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/proyectos
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _repository.ObtenerTodos());
        }

        // GET: api/proyectos/cliente/1
        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> ObtenerPorCliente(int clienteId)
        {
            var proyectos = await _repository.ObtenerPorCliente(clienteId);
            return Ok(proyectos);
        }

        // POST: api/proyectos
        [HttpPost]
        public async Task<IActionResult> Crear(Proyecto proyecto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _repository.Crear(proyecto);
            return Ok(new { mensaje = "Proyecto creado", datos = proyecto });
        }

        // PUT: api/proyectos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, Proyecto proyecto)
        {
            if (id != proyecto.Id) return BadRequest();

            var existente = await _repository.ObtenerPorId(id);
            if (existente == null) return NotFound();

            existente.Nombre = proyecto.Nombre;
            existente.Descripcion = proyecto.Descripcion;
            existente.Precio = proyecto.Precio;

            await _repository.Actualizar(existente);
            return NoContent();
        }

        // DELETE: api/proyectos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existente = await _repository.ObtenerPorId(id);
            if (existente == null) return NotFound();

            await _repository.Eliminar(id);
            return NoContent();
        }
    }
}