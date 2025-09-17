
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Auditoria
    {
        [Key]
        public int IdAuditoria { get; set; }
        public int IdUsuario { get; set; }
        public string? EntidadAfectada { get; set; }
        public string? Operacion { get; set; }
        public DateTime Fecha { get; set; }
        public string? Detalle { get; set; }
        [ForeignKey("IdUsuario")] public Usuarios? _Usuario { get; set; }

    }
}
