using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IDeudasAplicacion
    {
        void Configurar(string StringConexion);
        List<Deudas> Listar();
        Deudas? Guardar(Deudas? entidad);
        Deudas? Modificar(Deudas? entidad);
        Deudas? Borrar(Deudas? entidad);
    }
}
