using Application.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.Proyectos
{
    public interface IEditarProyectoUseCase { Task Execute(Proyecto p); }

    public class EditarProyectoUseCase : IEditarProyectoUseCase
    {
        private readonly IProyectoRepository _repo;
        public EditarProyectoUseCase(IProyectoRepository repo) => _repo = repo;

        public async Task Execute(Proyecto p) => await _repo.Editar(p);
    }
}