using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.UseCases; // <--- ESTA ERA LA CLAVE QUE FALTABA
using Application.UseCases.Clientes;
using Domain.Entities;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IObtenerClientesUseCase _obtener;
        private readonly ICrearClienteUseCase _crear;
        private readonly IEditarClienteUseCase _editar;
        private readonly IEliminarClienteUseCase _eliminar;

        public ClientesController(
            IObtenerClientesUseCase obtener, ICrearClienteUseCase crear,
            IEditarClienteUseCase editar, IEliminarClienteUseCase eliminar)
        {
            _obtener = obtener;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _obtener.Ejecutar());

        [HttpPost]
        public async Task<IActionResult> Create(Cliente c) { await _crear.Execute(c); return Ok(); }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cliente c)
        {
            c.Id = id;
            await _editar.Execute(c);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) { await _eliminar.Execute(id); return Ok(); }
    }
}