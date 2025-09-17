

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Suscripciones
    {
        [Key]
        public int IdSuscripcion { get; set; }
        public int IdUsuario { get; set; }
        public int IdProveedor { get; set; }
        public decimal MontoMensual { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaRenovacion { get; set; }

        [ForeignKey("IdUsuario")] public Usuarios? _Usuario { get; set; }
        [ForeignKey("IdProveedor")] public ProveedoresServicios? _Proveedor { get; set; }


    }
}
