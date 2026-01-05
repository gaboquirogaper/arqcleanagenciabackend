namespace Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string NombreEmpresa { get; set; } = string.Empty; // Ej: Buena Vida
        public string ContactoNombre { get; set; } = string.Empty; // Ej: Juan Perez
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
    }
}
