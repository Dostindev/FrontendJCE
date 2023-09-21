using System.ComponentModel.DataAnnotations;

namespace FrontendApi.Models
{
    public class Estudiante
    {
        public int IdEstudiante { get; set; }
        public string? Nombres { get; set; } = null!;
        public string? Apellidos { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }

        public int? CursoId { get; set; }
        public Curso? Curso { get; set; }
    }
}
