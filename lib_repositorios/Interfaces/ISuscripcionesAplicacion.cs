using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface ISuscripcionesAplicacion
    {
        void Configurar(string StringConexion);
        List<Suscripciones> Listar();
        Suscripciones? Guardar(Suscripciones? entidad);
        Suscripciones? Modificar(Suscripciones? entidad);
        Suscripciones? Borrar(Suscripciones? entidad);
    }
}
