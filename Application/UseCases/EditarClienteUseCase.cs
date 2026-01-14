using Application.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.Clientes
{
    public interface IEditarClienteUseCase { Task Execute(Cliente cliente); }

    public class EditarClienteUseCase : IEditarClienteUseCase
    {
        private readonly IClienteRepository _repository;
        public EditarClienteUseCase(IClienteRepository repository) => _repository = repository;

        public async Task Execute(Cliente cliente)
        {
            await _repository.Editar(cliente);
        }
    }
}