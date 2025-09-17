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

            //OPERACIONES
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

            //OPERACIONES
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

            //OPERACIONES
            entidad._Deuda = null;

            var entry = this.IConexion!.Entry<PagosDeuda>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<PagosDeuda> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
