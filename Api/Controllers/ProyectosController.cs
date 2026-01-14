using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Proyectos;
using Domain.Entities;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly IObtenerProyectosUseCase _obtener;
        private readonly ICrearProyectoUseCase _crear;
        private readonly IEditarProyectoUseCase _editar;      // Nuevo
        private readonly IEliminarProyectoUseCase _eliminar;  // Nuevo

        public ProyectosController(
            IObtenerProyectosUseCase obtener,
            ICrearProyectoUseCase crear,
            IEditarProyectoUseCase editar,
            IEliminarProyectoUseCase eliminar)
        {
            _obtener = obtener;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _obtener.Execute());

        [HttpPost]
        public async Task<IActionResult> Create(Proyecto p)
        {
            await _crear.Execute(p);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Proyecto p)
        {
            p.Id = id;
            await _editar.Execute(p);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _eliminar.Execute(id);
            return Ok();
        }
    }
}