using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IMovimientosTarjetaAplicacion
    {
        void Configurar(string StringConexion);
        List<MovimientosTarjeta> Listar();
        MovimientosTarjeta? Guardar(MovimientosTarjeta? entidad);
        MovimientosTarjeta? Modificar(MovimientosTarjeta? entidad);
        MovimientosTarjeta? Borrar(MovimientosTarjeta? entidad);
    }
}
