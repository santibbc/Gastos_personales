

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Deudas
    {
        [Key]
        public int IdDeuda { get; set; }
        public int IdUsuario { get; set; }
        public string? Acreedor { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string? Estado { get; set; }

        [ForeignKey("IdUsuario")] public Usuarios? _Usuario { get; set; }

    }
}
