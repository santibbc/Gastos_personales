using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IAuditoriaAplicacion
    {
        void Configurar(string StringConexion);
        List<Auditoria> Listar();
        Auditoria? Guardar(Auditoria? entidad);
        Auditoria? Modificar(Auditoria? entidad);
        Auditoria? Borrar(Auditoria? entidad);
    }
}
