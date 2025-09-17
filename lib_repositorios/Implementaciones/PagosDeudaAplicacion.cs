using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class PagosDeudaAplicacion : IPagosDeudaAplicacion
    {
        private IConexion? IConexion = null;

        public PagosDeudaAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public PagosDeuda? Borrar(PagosDeuda? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdPago == 0)
                throw new Exception("lbNoSeGuardo");

            // no borrar pagos realizados en el último mes
            if (entidad.Fecha > DateTime.Now.AddMonths(-1))
                throw new Exception("No se pueden borrar pagos recientes (último mes).");

            entidad._Deuda = null;

            this.IConexion!.PagosDeuda!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public PagosDeuda? Guardar(PagosDeuda? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdPago != 0)
                throw new Exception("lbYaSeGuardo");

            // validar que la deuda exista
            if (entidad.Monto <= 0)
                throw new Exception("El monto del pago debe ser mayor a 0.");

            // validar que la deuda exista
            if (string.IsNullOrWhiteSpace(entidad.MetodoPago))
                throw new Exception("Debe especificar el método de pago.");

            // la fecha no puede ser futura
            if (entidad.Fecha > DateTime.Now)
                throw new Exception("La fecha del pago no puede estar en el futuro.");

            entidad._Deuda = null;

            this.IConexion!.PagosDeuda!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public PagosDeuda? Modificar(PagosDeuda? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdPago == 0)
                throw new Exception("lbNoSeGuardo");

            // no puede modificar pagos con más de 1 mes de antigüedad
            var existente = this.IConexion!.PagosDeuda!.AsNoTracking()
                .FirstOrDefault(p => p.IdPago == entidad.IdPago);

            if (existente != null && existente.Fecha < DateTime.Now.AddMonths(-1))
                throw new Exception("No se pueden modificar pagos antiguos (ya conciliados).");

            // monto no puede ser negativo
            if (entidad.Monto < 0)
                throw new Exception("El monto del pago no puede ser negativo.");

            entidad._Deuda = null;

            var entry = this.IConexion!.Entry<PagosDeuda>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<PagosDeuda> Listar()
        {
            // listar los últimos 100 pagos realizados, ordenados por fecha descendente
            return this.IConexion!.PagosDeuda!
                .OrderByDescending(p => p.Fecha)
                .Take(100)
                .ToList();
        }
    }
}

