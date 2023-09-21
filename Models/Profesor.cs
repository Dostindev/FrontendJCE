using System.ComponentModel.DataAnnotations;

namespace FrontendApi.Models
{
    public class Profesor
    {
        public int IdProfesor { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public bool Activo { get; set; }
        public List<Curso>? Cursos { get; set; }
    }


}

