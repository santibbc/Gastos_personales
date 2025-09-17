

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Presupuestos
    {
        [Key]
        public int IdPresupuesto { get; set; }
        public int IdUsuario { get; set; }
        public int IdCategoria { get; set; }
        public decimal MontoAsignado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        [ForeignKey("IdUsuario")] public Usuarios? _Usuario { get; set; }
        [ForeignKey("IdCategoria")] public CategoriasGasto? _Categoria { get; set; }


    }
}

