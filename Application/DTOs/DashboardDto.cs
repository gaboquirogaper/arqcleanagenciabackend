namespace Application.DTOs
{
    public class DashboardDto
    {
        public int TotalClientes { get; set; }
        public int TotalProyectos { get; set; }
        public decimal IngresosTotales { get; set; }
        public string? ClienteMasFrecuente { get; set; } // <--- EL SIGNO DE INTERROGACIÓN ARREGLA EL ERROR
    }
}