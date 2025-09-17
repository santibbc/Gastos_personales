using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface ITarjetasCreditoAplicacion
    {
        void Configurar(string StringConexion);
        List<TarjetasCredito> Listar();
        TarjetasCredito? Guardar(TarjetasCredito? entidad);
        TarjetasCredito? Modificar(TarjetasCredito? entidad);
        TarjetasCredito? Borrar(TarjetasCredito? entidad);
    }
}
