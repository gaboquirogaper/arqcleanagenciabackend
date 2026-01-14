using Application.Interfaces;
using Application.Interfaces.UseCases;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.UseCases.Clientes // Nota el namespace
{
    public class ObtenerClientesUseCase : IObtenerClientesUseCase
    {
        private readonly IClienteRepository _repository;

        public ObtenerClientesUseCase(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Cliente>> Ejecutar()
        {
            return await _repository.ObtenerTodos();
        }
    }
}