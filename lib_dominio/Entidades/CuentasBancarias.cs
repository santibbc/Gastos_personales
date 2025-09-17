

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class CuentasBancarias
    {
        [Key]
        public int IdCuenta { get; set; }
        public int IdUsuario { get; set; }
        public string? Banco { get; set; }
        public string? NumeroCuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public string? Moneda { get; set; }
        [ForeignKey("IdUsuario")] public Usuarios? _Usuario { get; set; }

    }
}
