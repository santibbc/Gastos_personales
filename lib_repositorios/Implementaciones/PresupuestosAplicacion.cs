using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class PresupuestosAplicacion : IPresupuestosAplicacion
    {
        private IConexion? IConexion = null;

        public PresupuestosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Presupuestos? Borrar(Presupuestos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdPresupuesto == 0)
                throw new Exception("lbNoSeGuardo");

            // no borrar presupuestos activos
            if (entidad.FechaFin >= DateTime.Now)
                throw new Exception("No se pueden borrar presupuestos activos.");

            entidad._Usuario = null;
            entidad._Categoria = null;

            this.IConexion!.Presupuestos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Presupuestos? Guardar(Presupuestos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdPresupuesto != 0)
                throw new Exception("lbYaSeGuardo");

            // monto asignado, fecha inicio y fecha fin son obligatorios
            if (entidad.MontoAsignado <= 0)
                throw new Exception("El monto asignado del presupuesto debe ser mayor a 0.");

            if (entidad.FechaInicio >= entidad.FechaFin)
                throw new Exception("La fecha de inicio debe ser menor a la fecha de fin.");

            entidad._Usuario = null;
            entidad._Categoria = null;

            this.IConexion!.Presupuestos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Presupuestos? Modificar(Presupuestos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdPresupuesto == 0)
                throw new Exception("lbNoSeGuardo");

            // monto asignado, fecha inicio y fecha fin son obligatorios
            if (entidad.MontoAsignado <= 0)
                throw new Exception("El monto asignado del presupuesto debe ser mayor a 0.");

            if (entidad.FechaInicio >= entidad.FechaFin)
                throw new Exception("La fecha de inicio debe ser menor a la fecha de fin.");

            entidad._Usuario = null;
            entidad._Categoria = null;

            var entry = this.IConexion!.Entry<Presupuestos>(entidad);
            entry.State = EntityState.Modified;

            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Presupuestos> Listar()
        {
            // mostrar solo los presupuestos activos ordenados por fecha de inicio ascendente
            return this.IConexion!.Presupuestos!
                .Where(p => p.FechaFin >= DateTime.Now)
                .OrderBy(p => p.FechaInicio)
                .ToList();
        }
    }
}

