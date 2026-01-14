using Application.Interfaces;
using System.Threading.Tasks;

namespace Application.UseCases.Clientes
{
    // Definimos la Interfaz aquí mismo para ir rápido
    public interface IEliminarClienteUseCase { Task Execute(int id); }

    public class EliminarClienteUseCase : IEliminarClienteUseCase
    {
        private readonly IClienteRepository _repository;
        public EliminarClienteUseCase(IClienteRepository repository) => _repository = repository;

        public async Task Execute(int id)
        {
            await _repository.Eliminar(id);
        }
    }
}