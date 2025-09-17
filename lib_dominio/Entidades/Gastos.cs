
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Gastos
    {
        [Key]
        public int IdGasto { get; set; }
        public int IdUsuario { get; set; }
        public int IdCategoria { get; set; }
        public int IdCuenta { get; set; }
        public decimal Monto { get; set; }
        public string? Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("IdUsuario")] public Usuarios? _Usuario { get; set; }
        [ForeignKey("IdCategoria")] public CategoriasGasto? _Categoria { get; set; }
        [ForeignKey("IdCuenta")] public CuentasBancarias? _Cuenta { get; set; }



    }
}
