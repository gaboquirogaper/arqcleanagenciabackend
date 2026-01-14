using Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.UseCases.Proyectos
{
    public interface IObtenerProyectosUseCase { Task<List<Proyecto>> Execute(); }

    public class ObtenerProyectosUseCase : IObtenerProyectosUseCase
    {
        private readonly IProyectoRepository _repo;
        public ObtenerProyectosUseCase(IProyectoRepository repo) => _repo = repo;

        public async Task<List<Proyecto>> Execute()
        {
            return await _repo.ObtenerTodos();
        }
    }
}