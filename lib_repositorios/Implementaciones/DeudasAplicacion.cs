using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class DeudasAplicacion : IDeudasAplicacion
    {
        private IConexion? IConexion = null;

        public DeudasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Deudas? Borrar(Deudas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdDeuda == 0)
                throw new Exception("lbNoSeGuardo");

            // no se puede borrar una deuda activa
            if (entidad.Estado != null && entidad.Estado.ToLower() == "activa")
                throw new Exception("No se pueden borrar deudas activas.");

            entidad._Usuario = null;

            this.IConexion!.Deudas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Deudas? Guardar(Deudas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdDeuda != 0)
                throw new Exception("lbYaSeGuardo");

            // acreedor y monto son obligatorios
            if (string.IsNullOrWhiteSpace(entidad.Acreedor))
                throw new Exception("El acreedor es obligatorio.");

            if (entidad.Monto <= 0)
                throw new Exception("El monto de la deuda debe ser mayor a cero.");

            // fecha de inicio por defecto si no se proporciona
            if (entidad.FechaVencimiento <= entidad.FechaInicio)
                throw new Exception("La fecha de vencimiento debe ser mayor a la fecha de inicio.");

            // Si no se asigna estado, lo dejamos como "Activa"
            if (string.IsNullOrWhiteSpace(entidad.Estado))
                entidad.Estado = "Activa";

            entidad._Usuario = null;

            this.IConexion!.Deudas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Deudas? Modificar(Deudas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdDeuda == 0)
                throw new Exception("lbNoSeGuardo");

            // no permitir modificar acreedor ni monto a negativo
            if (entidad.Monto < 0)
                throw new Exception("El monto de la deuda no puede ser negativo.");

            // no permitir cambiar el acreedor una vez creada la deuda
            var existente = this.IConexion!.Deudas!.AsNoTracking()
                .FirstOrDefault(d => d.IdDeuda == entidad.IdDeuda);

            if (existente != null && existente.Acreedor != entidad.Acreedor)
                throw new Exception("No está permitido cambiar el acreedor de la deuda.");

            entidad._Usuario = null;

            var entry = this.IConexion!.Entry<Deudas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Deudas> Listar()
        {
            // listar primero las deudas activas, luego por fecha de vencimiento ascendente
            return this.IConexion!.Deudas!
                .OrderByDescending(d => d.Estado == "Activa")
                .ThenBy(d => d.FechaVencimiento)
                .ToList();
        }
    }
}
