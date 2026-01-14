using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProyectoRepository
    {
        Task<List<Proyecto>> ObtenerTodos();
        Task Crear(Proyecto proyecto);

        // --- NUEVOS ---
        Task Editar(Proyecto proyecto);
        Task Eliminar(int id);
    }
}