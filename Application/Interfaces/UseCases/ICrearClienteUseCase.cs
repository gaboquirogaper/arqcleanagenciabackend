using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases
{
    public interface ICrearClienteUseCase
    {
        Task Execute(Cliente cliente);
    }
}