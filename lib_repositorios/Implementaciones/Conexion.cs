using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public partial class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        public Conexion() { }

        public Conexion(DbContextOptions<Conexion> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(this.StringConexion))
            {
                optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

   
        public DbSet<Usuarios>? Usuarios { get; set; }

        public DbSet<CuentasBancarias>? CuentasBancarias { get; set; }

        public DbSet<CategoriasGasto>? CategoriasGasto { get; set; }

        public DbSet<Gastos>? Gastos { get; set; }

        public DbSet<Ingresos>? Ingresos { get; set; }

        public DbSet<Presupuestos>? Presupuestos { get; set; }

        public DbSet<MetasAhorro>? MetasAhorro { get; set; }

        public DbSet<TarjetasCredito>? TarjetasCredito { get; set; }

        public DbSet<MovimientosTarjeta>? MovimientosTarjeta { get; set; }

        public DbSet<Deudas>? Deudas { get; set; }

        public DbSet<PagosDeuda>? PagosDeuda { get; set; }

        public DbSet<ConfiguracionUsuario>? ConfiguracionUsuario { get; set; }

        public DbSet<ProveedoresServicios>? ProveedoresServicios { get; set; }

        public DbSet<Suscripciones>? Suscripciones { get; set; }

        public DbSet<Auditoria>? Auditoria { get; set; }
    }
}

