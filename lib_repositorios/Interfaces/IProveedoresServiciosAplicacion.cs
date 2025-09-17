using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IProveedoresServiciosAplicacion
    {
        void Configurar(string StringConexion);
        List<ProveedoresServicios> Listar();
        ProveedoresServicios? Guardar(ProveedoresServicios? entidad);
        ProveedoresServicios? Modificar(ProveedoresServicios? entidad);
        ProveedoresServicios? Borrar(ProveedoresServicios? entidad);
    }
}
