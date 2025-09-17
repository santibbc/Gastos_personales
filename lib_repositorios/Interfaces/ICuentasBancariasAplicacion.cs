using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface ICuentasBancariasAplicacion
    {
        void Configurar(string StringConexion);
        List<CuentasBancarias> Listar();
        CuentasBancarias? Guardar(CuentasBancarias? entidad);
        CuentasBancarias? Modificar(CuentasBancarias? entidad);
        CuentasBancarias? Borrar(CuentasBancarias? entidad);
    }
}