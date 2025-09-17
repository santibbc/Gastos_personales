using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IPresupuestosAplicacion
    {
        void Configurar(string StringConexion);
        List<Presupuestos> Listar();
        Presupuestos? Guardar(Presupuestos? entidad);
        Presupuestos? Modificar(Presupuestos? entidad);
        Presupuestos? Borrar(Presupuestos? entidad);
    }
}
