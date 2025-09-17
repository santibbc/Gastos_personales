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

            //OPERACIONES
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

            //OPERACIONES
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

            //OPERACIONES
            entidad._Usuario = null;

            var entry = this.IConexion!.Entry<TarjetasCredito>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<TarjetasCredito> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
