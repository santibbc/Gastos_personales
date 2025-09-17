

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class ConfiguracionUsuario
    {
        [Key]
        public int IdConfig { get; set; }
        public int IdUsuario { get; set; }
        public string? MonedaPreferida { get; set; }
        public bool Notificaciones { get; set; }
        public decimal? LimiteAlertas { get; set; }
        [ForeignKey("IdUsuario")] public Usuarios? _Usuario { get; set; }


    }
}
