using System.ComponentModel.DataAnnotations;

namespace FrontendApi.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string? Nombres { get; set; } = null!;
        public string? Apellidos { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }
        public int? ProyectoId { get; set; }
        public Proyecto? Proyecto { get; set; }
    }
}
