

using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class ProveedoresServicios
    {
        [Key]
        public int IdProveedor { get; set; }
        public string? Nombre { get; set; }
        public string? Categoria { get; set; }
        public string? Contacto { get; set; }
    }
}
