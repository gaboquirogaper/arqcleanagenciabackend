using Application.Interfaces;
using System.Threading.Tasks;

namespace Application.UseCases.Proyectos
{
    public interface IEliminarProyectoUseCase { Task Execute(int id); }

    public class EliminarProyectoUseCase : IEliminarProyectoUseCase
    {
        private readonly IProyectoRepository _repo;
        public EliminarProyectoUseCase(IProyectoRepository repo) => _repo = repo;

        public async Task Execute(int id) => await _repo.Eliminar(id);
    }
}