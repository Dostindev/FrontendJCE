using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrontendApi.Models
{
    public class Proyecto
    {
        public int IdProyecto { get; set; }
        public string? Descripción { get; set; }
        public DateTime Fecha { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public List<Cliente>? Clientes { get; set; }
    }
}
