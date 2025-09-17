using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace lib_repositorios.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Usuarios>? Usuarios { get; set; }
        DbSet<CuentasBancarias>? CuentasBancarias { get; set; }
        DbSet<CategoriasGasto>? CategoriasGasto { get; set; }
        DbSet<Gastos>? Gastos { get; set; }
        DbSet<Ingresos>? Ingresos { get; set; }
        DbSet<Presupuestos>? Presupuestos { get; set; }
        DbSet<MetasAhorro>? MetasAhorro { get; set; }
        DbSet<TarjetasCredito>? TarjetasCredito { get; set; }
        DbSet<MovimientosTarjeta>? MovimientosTarjeta { get; set; }
        DbSet<Deudas>? Deudas { get; set; }
        DbSet<PagosDeuda>? PagosDeuda { get; set; }
        DbSet<ConfiguracionUsuario>? ConfiguracionUsuario { get; set; }
        DbSet<ProveedoresServicios>? ProveedoresServicios { get; set; }
        DbSet<Suscripciones>? Suscripciones { get; set; }
        DbSet<Auditoria>? Auditoria { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}

