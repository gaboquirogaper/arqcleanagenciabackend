using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases
{
    public interface IObtenerClientesUseCase
    {
        Task<List<Cliente>> Ejecutar();
    }
}