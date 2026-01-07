using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObtenerTodos();
        Task<Cliente?> ObtenerPorId(int id); 
        Task Crear(Cliente cliente);
        Task Actualizar(Cliente cliente);    
        Task Eliminar(int id);               
    }
}