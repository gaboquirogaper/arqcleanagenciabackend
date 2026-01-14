using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Proyecto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El proyecto necesita un nombre.")]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        [Range(1, 1000000, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Precio { get; set; }

        
        [Required]
        public int ClienteId { get; set; }
    }
}