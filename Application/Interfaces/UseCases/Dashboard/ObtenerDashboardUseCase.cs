using Application.Interfaces;
using Application.DTOs; // Asegurate que tu DTO esté en este namespace
using System.Threading.Tasks;
using System.Linq;

namespace Application.UseCases.Dashboard
{
	public interface IObtenerDashboardUseCase { Task<DashboardDto> Execute(); }

	public class ObtenerDashboardUseCase : IObtenerDashboardUseCase
	{
		private readonly IClienteRepository _clienteRepo;
		private readonly IProyectoRepository _proyectoRepo;

		public ObtenerDashboardUseCase(IClienteRepository clienteRepo, IProyectoRepository proyectoRepo)
		{
			_clienteRepo = clienteRepo;
			_proyectoRepo = proyectoRepo;
		}

		public async Task<DashboardDto> Execute()
		{
			var clientes = await _clienteRepo.ObtenerTodos();
			var proyectos = await _proyectoRepo.ObtenerTodos();

			return new DashboardDto
			{
				TotalClientes = clientes.Count,
				TotalProyectos = proyectos.Count,
				IngresosTotales = proyectos.Sum(p => p.Precio),
				ClienteMasFrecuente = "Juan Perez" // Lógica simple por ahora
			};
		}
	}
}