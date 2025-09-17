

using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class CategoriasGasto
    {
        [Key]
        public int IdCategoria { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
