using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class SuscripcionesAplicacion : ISuscripcionesAplicacion
    {
        private IConexion? IConexion = null;

        public SuscripcionesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Suscripciones? Borrar(Suscripciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdSuscripcion == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;
            entidad._Proveedor = null;



            this.IConexion!.Suscripciones!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Suscripciones? Guardar(Suscripciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdSuscripcion != 0)
                throw new Exception("lbYaSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;
            entidad._Proveedor = null;

            this.IConexion!.Suscripciones!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Suscripciones? Modificar(Suscripciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdSuscripcion == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;
            entidad._Proveedor = null;

            var entry = this.IConexion!.Entry<Suscripciones>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Suscripciones> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
