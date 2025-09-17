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

            // no borrar suscripciones activas
            if (entidad.FechaRenovacion > DateTime.Now)
                throw new Exception("No se puede eliminar una suscripción activa.");

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

            // monto mensual debe ser mayor a 0
            if (entidad.MontoMensual <= 0)
                throw new Exception("El monto mensual debe ser mayor a 0.");

            if (entidad.FechaInicio >= entidad.FechaRenovacion)
                throw new Exception("La fecha de inicio debe ser menor a la fecha de renovación.");

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

            // monto mensual debe ser positivo
            if (entidad.MontoMensual <= 0)
                throw new Exception("El monto mensual debe ser mayor a 0.");

            if (entidad.FechaInicio >= entidad.FechaRenovacion)
                throw new Exception("La fecha de inicio debe ser menor a la fecha de renovación.");

            entidad._Usuario = null;
            entidad._Proveedor = null;

            var entry = this.IConexion!.Entry<Suscripciones>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Suscripciones> Listar()
        {
            // listamos solo las suscripciones activas (con fecha de renovación futura)
            return this.IConexion!.Suscripciones!
                .Where(s => s.FechaRenovacion >= DateTime.Now)
                .OrderBy(s => s.FechaRenovacion)
                .ToList();
        }
    }
}

