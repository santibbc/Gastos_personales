

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class MovimientosTarjeta
    {
        [Key]
        public int IdMovimiento { get; set; }
        public int IdTarjeta { get; set; }
        public string? Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        
        [ForeignKey("IdTarjeta")] public TarjetasCredito? _Tarjeta { get; set; }


    }
}
