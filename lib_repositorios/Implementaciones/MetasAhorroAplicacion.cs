using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class MetasAhorroAplicacion : IMetasAhorroAplicacion
    {
        private IConexion? IConexion = null;

        public MetasAhorroAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public MetasAhorro? Borrar(MetasAhorro? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdMeta == 0)
                throw new Exception("lbNoSeGuardo");

            // no se pueden borrar metas activas
            if (entidad.Estado != null && entidad.Estado.ToLower() == "activa")
                throw new Exception("No se puede eliminar una meta activa.");

            entidad._Usuario = null;

            this.IConexion!.MetasAhorro!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public MetasAhorro? Guardar(MetasAhorro? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdMeta != 0)
                throw new Exception("lbYaSeGuardo");

            // nombre y monto objetivo son obligatorios
            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("Debe asignar un nombre a la meta de ahorro.");

            if (entidad.MontoObjetivo <= 0)
                throw new Exception("El monto objetivo debe ser mayor a cero.");

            // la fecha límite debe ser futura
            if (entidad.FechaLimite <= DateTime.Now)
                throw new Exception("La fecha límite de la meta debe ser mayor a la fecha actual.");

            // si no se asigna estado, lo dejamos como "Activa"
            if (string.IsNullOrWhiteSpace(entidad.Estado))
                entidad.Estado = "Activa";

            entidad._Usuario = null;

            this.IConexion!.MetasAhorro!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public MetasAhorro? Modificar(MetasAhorro? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdMeta == 0)
                throw new Exception("lbNoSeGuardo");

            // no permitir cambiar el nombre a vacío
            if (entidad.MontoObjetivo < 0)
                throw new Exception("El monto objetivo no puede ser negativo.");

            //no permitir cambiar la fecha límite a una pasada
            if (entidad.FechaLimite < DateTime.Now)
                throw new Exception("La fecha límite no puede estar en el pasado.");

            entidad._Usuario = null;

            var entry = this.IConexion!.Entry<MetasAhorro>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<MetasAhorro> Listar()
        {
            // listar solo las metas activas ordenadas por fecha límite ascendente
            return this.IConexion!.MetasAhorro!
                .Where(m => m.Estado == "Activa")
                .OrderBy(m => m.FechaLimite)
                .ToList();
        }
    }
}

