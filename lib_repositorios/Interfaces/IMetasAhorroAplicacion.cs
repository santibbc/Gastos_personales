using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IMetasAhorroAplicacion
    {
        void Configurar(string StringConexion);
        List<MetasAhorro> Listar();
        MetasAhorro? Guardar(MetasAhorro? entidad);
        MetasAhorro? Modificar(MetasAhorro? entidad);
        MetasAhorro? Borrar(MetasAhorro? entidad);
    }
}
