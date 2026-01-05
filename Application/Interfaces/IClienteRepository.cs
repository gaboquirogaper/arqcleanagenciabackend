using Domain.Entities;

namespace Application.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObtenerTodos();
        Task Crear(Cliente cliente);
    }
}