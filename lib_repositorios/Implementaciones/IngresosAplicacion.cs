using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class IngresosAplicacion : IIngresosAplicacion
    {
        private IConexion? IConexion = null;

        public IngresosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Ingresos? Borrar(Ingresos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdIngreso == 0)
                throw new Exception("lbNoSeGuardo");

            // no permitir borrar ingresos del mes actual
            if (entidad.Fecha.Month == DateTime.Now.Month && entidad.Fecha.Year == DateTime.Now.Year)
                throw new Exception("No se pueden borrar ingresos registrados en el mes actual.");

            entidad._Usuario = null;
            entidad._Cuenta = null;

            this.IConexion!.Ingresos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Ingresos? Guardar(Ingresos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdIngreso != 0)
                throw new Exception("lbYaSeGuardo");

            // monto es obligatorio y mayor a 0
            if (entidad.Monto <= 0)
                throw new Exception("El monto del ingreso debe ser mayor a 0.");

            // fuente es obligatoria
            if (string.IsNullOrWhiteSpace(entidad.Fuente))
                throw new Exception("Debe especificar la fuente del ingreso.");

            // no puede ser en el futuro
            if (entidad.Fecha > DateTime.Now)
                throw new Exception("La fecha del ingreso no puede ser en el futuro.");

            entidad._Usuario = null;
            entidad._Cuenta = null;

            this.IConexion!.Ingresos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Ingresos? Modificar(Ingresos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdIngreso == 0)
                throw new Exception("lbNoSeGuardo");

            // no permitir cambiar la fuente de un ingreso ya registrado
            var existente = this.IConexion!.Ingresos!.AsNoTracking()
                .FirstOrDefault(i => i.IdIngreso == entidad.IdIngreso);

            if (existente != null && existente.Fuente != entidad.Fuente)
                throw new Exception("No se puede cambiar la fuente de un ingreso ya registrado.");

            // no permitir modificar ingresos con fecha en el futuro
            if (entidad.Monto < 0)
                throw new Exception("El monto del ingreso no puede ser negativo.");

            entidad._Usuario = null;
            entidad._Cuenta = null;

            var entry = this.IConexion!.Entry<Ingresos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Ingresos> Listar()
        {
            // listar los últimos 50 ingresos ordenados por fecha descendente
            return this.IConexion!.Ingresos!
                .OrderByDescending(i => i.Fecha)
                .Take(50)
                .ToList();
        }
    }
}
