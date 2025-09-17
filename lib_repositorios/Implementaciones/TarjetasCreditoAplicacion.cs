using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class TarjetasCreditoAplicacion : ITarjetasCreditoAplicacion
    {
        private IConexion? IConexion = null;

        public TarjetasCreditoAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public TarjetasCredito? Borrar(TarjetasCredito? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdTarjeta == 0)
                throw new Exception("lbNoSeGuardo");

            // no borrar tarjetas con cupo > 0
            if (entidad.Cupo > 0)
                throw new Exception("No se puede eliminar una tarjeta con cupo disponible.");

            entidad._Usuario = null;

            this.IConexion!.TarjetasCredito!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public TarjetasCredito? Guardar(TarjetasCredito? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdTarjeta != 0)
                throw new Exception("lbYaSeGuardo");

            // tarjeta debe tener banco, número, cupo, fecha corte y fecha pago
            if (string.IsNullOrWhiteSpace(entidad.Banco))
                throw new Exception("La tarjeta debe estar asociada a un banco válido.");

            if (string.IsNullOrWhiteSpace(entidad.NumeroTarjeta) || entidad.NumeroTarjeta.Length < 8)
                throw new Exception("El número de tarjeta debe tener al menos 8 dígitos.");

            if (entidad.Cupo <= 0)
                throw new Exception("El cupo de la tarjeta debe ser mayor a 0.");

            if (entidad.FechaCorte >= entidad.FechaPago)
                throw new Exception("La fecha de corte debe ser anterior a la fecha de pago.");

            entidad._Usuario = null;

            this.IConexion!.TarjetasCredito!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public TarjetasCredito? Modificar(TarjetasCredito? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdTarjeta == 0)
                throw new Exception("lbNoSeGuardo");

            // tarjetas con cupo <= 0 no se pueden modificar
            if (string.IsNullOrWhiteSpace(entidad.Banco))
                throw new Exception("La tarjeta debe estar asociada a un banco válido.");

            if (string.IsNullOrWhiteSpace(entidad.NumeroTarjeta) || entidad.NumeroTarjeta.Length < 8)
                throw new Exception("El número de tarjeta debe tener al menos 8 dígitos.");

            if (entidad.Cupo <= 0)
                throw new Exception("El cupo de la tarjeta debe ser mayor a 0.");

            if (entidad.FechaCorte >= entidad.FechaPago)
                throw new Exception("La fecha de corte debe ser anterior a la fecha de pago.");

            entidad._Usuario = null;

            var entry = this.IConexion!.Entry<TarjetasCredito>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<TarjetasCredito> Listar()
        {
            // mostrar solo las tarjetas con cupo > 0 ordenadas por banco
            return this.IConexion!.TarjetasCredito!
                .Where(t => t.Cupo > 0)
                .OrderBy(t => t.Banco)
                .ToList();
        }
    }
}

