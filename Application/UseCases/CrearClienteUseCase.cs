using Application.Interfaces;
using Application.Interfaces.UseCases;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.Clientes
{
    public class CrearClienteUseCase : ICrearClienteUseCase
    {
        private readonly IClienteRepository _repository;

        public CrearClienteUseCase(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(Cliente cliente)
        {
            await _repository.Crear(cliente);
        }
    }
}