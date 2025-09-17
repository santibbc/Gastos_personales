using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IGastosAplicacion
    {
        void Configurar(string StringConexion);
        List<Gastos> Listar();
        Gastos? Guardar(Gastos? entidad);
        Gastos? Modificar(Gastos? entidad);
        Gastos? Borrar(Gastos? entidad);
    }
}
