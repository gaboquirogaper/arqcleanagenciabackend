using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObtenerTodos();
        Task Crear(Cliente cliente);

        // --- AGREGAMOS ESTOS DOS ---
        Task Editar(Cliente cliente);
        Task Eliminar(int id);
    }
}