

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class TarjetasCredito
    {
        [Key]
        public int IdTarjeta { get; set; }
        public int IdUsuario { get; set; }
        public string? Banco { get; set; }
        public string? NumeroTarjeta { get; set; }
        public decimal Cupo { get; set; }
        public DateTime FechaCorte { get; set; }
        public DateTime FechaPago { get; set; }
        
        [ForeignKey("IdUsuario")] public Usuarios? _Usuario { get; set; }


    }
}
