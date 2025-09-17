using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IPagosDeudaAplicacion
    {
        void Configurar(string StringConexion);
        List<PagosDeuda> Listar();
        PagosDeuda? Guardar(PagosDeuda? entidad);
        PagosDeuda? Modificar(PagosDeuda? entidad);
        PagosDeuda? Borrar(PagosDeuda? entidad);
    }
}
