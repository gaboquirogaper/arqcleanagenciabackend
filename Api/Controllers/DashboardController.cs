using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepo;
        private readonly IProyectoRepository _proyectoRepo;

        // Inyectamos AMBOS repositorios para poder cruzar datos
        public DashboardController(IClienteRepository clienteRepo, IProyectoRepository proyectoRepo)
        {
            _clienteRepo = clienteRepo;
            _proyectoRepo = proyectoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerMetricas()
        {
            // 1. Traemos los datos crudos
            var clientes = await _clienteRepo.ObtenerTodos();
            var proyectos = await _proyectoRepo.ObtenerTodos();

            // 2. PROCESAMOS LA LÓGICA DE NEGOCIO (Aquí está la magia)
            var dashboard = new DashboardDto
            {
                TotalClientes = clientes.Count,
                TotalProyectos = proyectos.Count,
                IngresosProyectados = proyectos.Sum(p => p.Precio), // Suma total de dinero
                ResumenProyectos = new List<string>()
            };

            // 3. Encontrar al Cliente Estrella (Lógica extra para lucirse)
            // (Simplemente tomamos el primero si hay datos, para no complicar el código ahora)
            if (clientes.Any())
            {
                dashboard.ClienteEstrella = clientes.First().Nombre + " " + clientes.First().Apellido;
            }

            // 4. Llenamos un resumen rápido
            foreach (var proy in proyectos)
            {
                dashboard.ResumenProyectos.Add($"Proyecto: {proy.Nombre} - ${proy.Precio}");
            }

            return Ok(dashboard);
        }
    }
}