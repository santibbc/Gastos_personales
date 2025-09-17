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

            //OPERACIONES
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

            //OPERACIONES
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

            //OPERACIONES
            entidad._Usuario = null;

            var entry = this.IConexion!.Entry<CuentasBancarias>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<CuentasBancarias> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
