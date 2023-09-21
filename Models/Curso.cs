using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrontendApi.Models
{
    public class Curso
    {
     
        public int IdCurso { get; set; }
        public string? Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        public int? ProfesorId { get; set; }
        public Profesor? Profesor { get; set; }

        public List<Estudiante>? Estudiantes { get; set; }

    }
}
