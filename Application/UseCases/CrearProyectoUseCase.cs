using Application.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.Proyectos
{
    public interface ICrearProyectoUseCase { Task Execute(Proyecto p); }

    public class CrearProyectoUseCase : ICrearProyectoUseCase
    {
        private readonly IProyectoRepository _repo;
        public CrearProyectoUseCase(IProyectoRepository repo) => _repo = repo;

        public async Task Execute(Proyecto p)
        {
            await _repo.Crear(p);
        }
    }
}