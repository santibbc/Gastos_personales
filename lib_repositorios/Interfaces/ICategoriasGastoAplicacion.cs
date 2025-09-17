using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface ICategoriasGastoAplicacion
    {
        void Configurar(string StringConexion);
        List<CategoriasGasto> Listar();
        CategoriasGasto? Guardar(CategoriasGasto? entidad);
        CategoriasGasto? Modificar(CategoriasGasto? entidad);
        CategoriasGasto? Borrar(CategoriasGasto? entidad);
    }
}