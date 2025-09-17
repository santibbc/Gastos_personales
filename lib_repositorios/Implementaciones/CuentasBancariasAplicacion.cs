using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class CuentasBancariasAplicacion : ICuentasBancariasAplicacion
    {
        private IConexion? IConexion = null;

        public CuentasBancariasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public CuentasBancarias? Borrar(CuentasBancarias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdCuenta == 0)
                throw new Exception("lbNoSeGuardo");

            // no se puede borrar una cuenta con saldo distinto de cero
            if (entidad.SaldoInicial != 0)
                throw new Exception("No se puede eliminar una cuenta con saldo distinto de cero.");

            entidad._Usuario = null;

            this.IConexion!.CuentasBancarias!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public CuentasBancarias? Guardar(CuentasBancarias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdCuenta != 0)
                throw new Exception("lbYaSeGuardo");

            // validar campos obligatorios
            if (string.IsNullOrWhiteSpace(entidad.Banco))
                throw new Exception("El nombre del banco es obligatorio.");

            if (string.IsNullOrWhiteSpace(entidad.NumeroCuenta))
                throw new Exception("El número de cuenta es obligatorio.");

            // no permitir saldo inicial negativo
            bool existe = this.IConexion!.CuentasBancarias!
                .Any(c => c.NumeroCuenta == entidad.NumeroCuenta);

            if (existe)
                throw new Exception($"Ya existe una cuenta registrada con el número {entidad.NumeroCuenta}.");

            entidad._Usuario = null;

            this.IConexion!.CuentasBancarias!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public CuentasBancarias? Modificar(CuentasBancarias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdCuenta == 0)
                throw new Exception("lbNoSeGuardo");

            // el saldo inicial no puede ser negativo
            if (entidad.SaldoInicial < 0)
                throw new Exception("El saldo inicial no puede ser negativo.");

            entidad._Usuario = null;

            var entry = this.IConexion!.Entry<CuentasBancarias>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<CuentasBancarias> Listar()
        {
            // listar ordenado por banco
            return this.IConexion!.CuentasBancarias!
                .OrderBy(c => c.Banco)
                .ToList();
        }
    }
}

