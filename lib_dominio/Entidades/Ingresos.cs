using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Ingresos
    {
        [Key]
        public int IdIngreso { get; set; }
        public int IdUsuario { get; set; }
        public int IdCuenta { get; set; }
        public decimal Monto { get; set; }
        public string? Fuente { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("IdUsuario")] public Usuarios? _Usuario { get; set; }
        [ForeignKey("IdCuenta")] public CuentasBancarias? _Cuenta { get; set; }


    }
}
