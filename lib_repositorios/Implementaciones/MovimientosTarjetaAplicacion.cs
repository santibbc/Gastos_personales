using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class MovimientosTarjetaAplicacion : IMovimientosTarjetaAplicacion
    {
        private IConexion? IConexion = null;

        public MovimientosTarjetaAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public MovimientosTarjeta? Borrar(MovimientosTarjeta? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdMovimiento == 0)
                throw new Exception("lbNoSeGuardo");

            // no borrar movimientos con más de 6 meses de antigüedad
            if (entidad.Fecha < DateTime.Now.AddMonths(-6))
                throw new Exception("No se pueden borrar movimientos con más de 6 meses de antigüedad.");

            entidad._Tarjeta = null;

            this.IConexion!.MovimientosTarjeta!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public MovimientosTarjeta? Guardar(MovimientosTarjeta? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdMovimiento != 0)
                throw new Exception("lbYaSeGuardo");

            // validar que la tarjeta exista
            if (entidad.Monto == 0)
                throw new Exception("El monto del movimiento debe ser distinto de cero.");

            // descripción no puede ser vacía
            if (string.IsNullOrWhiteSpace(entidad.Descripcion))
                throw new Exception("Debe ingresar una descripción para el movimiento.");

            // fecha no puede ser futura
            if (entidad.Fecha > DateTime.Now)
                throw new Exception("La fecha del movimiento no puede ser en el futuro.");

            entidad._Tarjeta = null;

            this.IConexion!.MovimientosTarjeta!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public MovimientosTarjeta? Modificar(MovimientosTarjeta? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdMovimiento == 0)
                throw new Exception("lbNoSeGuardo");

            // no permitir monto negativo
            if (entidad.Monto < 0)
                throw new Exception("El monto del movimiento no puede ser negativo.");

            // no permitir cambiar la descripción a vacío
            var existente = this.IConexion!.MovimientosTarjeta!.AsNoTracking()
                .FirstOrDefault(m => m.IdMovimiento == entidad.IdMovimiento);

            if (existente != null && existente.Fecha != entidad.Fecha)
                throw new Exception("No se puede modificar la fecha del movimiento.");

            entidad._Tarjeta = null;

            var entry = this.IConexion!.Entry<MovimientosTarjeta>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<MovimientosTarjeta> Listar()
        {
            // listar los 100 movimientos más recientes
            return this.IConexion!.MovimientosTarjeta!
                .OrderByDescending(m => m.Fecha)
                .Take(100)
                .ToList();
        }
    }
}

