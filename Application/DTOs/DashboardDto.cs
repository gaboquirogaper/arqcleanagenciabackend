namespace Application.DTOs
{
    public class DashboardDto
    {
        public int TotalClientes { get; set; }
        public int TotalProyectos { get; set; }
        public decimal IngresosProyectados { get; set; } // Suma de dinero
        public string ClienteEstrella { get; set; } = string.Empty; // El que más proyectos tiene
        public List<string> ResumenProyectos { get; set; } = new List<string>();
    }
}