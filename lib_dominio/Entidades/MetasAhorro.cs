

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class MetasAhorro
    {
        [Key]
        public int IdMeta { get; set; }
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public decimal MontoObjetivo { get; set; }
        public DateTime FechaLimite { get; set; }
        public string? Estado { get; set; }

        [ForeignKey("IdUsuario")] public Usuarios? _Usuario { get; set; }

    }
}
