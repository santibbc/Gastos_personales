
using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Contrasena { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
    }
}
