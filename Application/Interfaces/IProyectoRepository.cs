using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProyectoRepository
    {
        Task<List<Proyecto>> ObtenerTodos();
        Task<List<Proyecto>> ObtenerPorCliente(int clienteId);
        Task<Proyecto?> ObtenerPorId(int id); 
        Task Crear(Proyecto proyecto);
        Task Actualizar(Proyecto proyecto);   
        Task Eliminar(int id);                
    }
}