using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IIngresosAplicacion
    {
        void Configurar(string StringConexion);
        List<Ingresos> Listar();
        Ingresos? Guardar(Ingresos? entidad);
        Ingresos? Modificar(Ingresos? entidad);
        Ingresos? Borrar(Ingresos? entidad);
    }
}
