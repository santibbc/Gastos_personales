using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IConfiguracionUsuarioAplicacion
    {
        void Configurar(string StringConexion);
        List<ConfiguracionUsuario> Listar();
        ConfiguracionUsuario? Guardar(ConfiguracionUsuario? entidad);
        ConfiguracionUsuario? Modificar(ConfiguracionUsuario? entidad);
        ConfiguracionUsuario? Borrar(ConfiguracionUsuario? entidad);
    }
}