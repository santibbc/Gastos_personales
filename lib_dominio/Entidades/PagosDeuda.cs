

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class PagosDeuda
    {
        [Key]
        public int IdPago { get; set; }
        public int IdDeuda { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string? MetodoPago { get; set; }

        [ForeignKey("IdDeuda")] public Deudas? _Deuda { get; set; }

    }
}
